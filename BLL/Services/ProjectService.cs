using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Enitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public ProjectService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        /// <summary>
        /// Add project to db
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(ProjectModel model)
        {
            if (model.CreationDate > model.ClosureDate)
            {
                throw new TaskTrackingException("Dates are invalid");
            }

            var element = _mapper.Map<Project>(model);
            await _uow.ProjectRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Gets manager id from model and change 
        /// managerID of project with id from model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProjectModel> AssignManagerToProject(AssignManagerToProjectModel model)
        {
            var res = await _uow.ProjectRepository.GetByIdAsync(model.ProjectId);
            res.ManagerId = model.ManagerId;
            _uow.ProjectRepository.Update(res);
            await _uow.SaveAsync();

            return _mapper.Map<ProjectModel>(res);
        }

        /// <summary>
        /// Removes project with id from db
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.ProjectRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Returns all project records from db
        /// </summary>
        /// <returns>Sequence of projects</returns>
        public IEnumerable<ProjectModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<ProjectModel>>(_uow.ProjectRepository.GetAllWithDetails());
            return res;
        }

        /// <summary>
        /// Returns project with assignment that user entered
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project that has corresponding assignment</returns>
        public ProjectModel GetByAssignmentId(int id)
        {
            var res = _uow.ProjectRepository.GetAllWithDetails().First(p => p.Assignments.Select(l => l.Id).Contains(id));
            return _mapper.Map<ProjectModel>(res);
        }

        /// <summary>
        /// Returns project with corresponding Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project with corresponding Id</returns>
        public async Task<ProjectModel> GetByIdAsync(int id)
        {
            var res = await _uow.ProjectRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<ProjectModel>(res);
        }

        /// <summary>
        /// Returns projects where corresponding employee works
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of projects where employee works</returns>
        public IEnumerable<ProjectModel> GetProjectsByEmployee(int id)
        {
            var res = _mapper.Map<IEnumerable<ProjectModel>>(_uow.ProjectRepository.GetAllWithDetails().Where(p => p.ManagerId == id));
            return res;
        }

        /// <summary>
        /// Changes project record with corresponding Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ProjectModel model) 
        {
            _uow.ProjectRepository.Update(_mapper.Map<Project>(model));
            await _uow.SaveAsync();
        }
    }
}
