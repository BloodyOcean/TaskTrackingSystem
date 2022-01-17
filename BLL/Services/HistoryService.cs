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
    public class HistoryService : IHistoryService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public HistoryService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        /// <summary>
        /// Add history to db
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(HistoryModel model)
        {
            var element = _mapper.Map<History>(model);
            var ass = await _uow.AssignmentRepository.GetByIdAsync((int)model.AssignmentId); // Corresponding assignment.

            if ((element.ChangeDate > ass.ClosureDate) || (element.ChangeDate < ass.CreationDate))
            {
                throw new TaskTrackingException("Invalid data");
            }

            await _uow.HistoryRepository.AddAsync(element);
            await _uow.SaveAsync();
        }


        /// <summary>
        /// Removes history with id from db
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.HistoryRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Returns all histories records from db
        /// </summary>
        /// <returns>Sequence of histories</returns>
        public IEnumerable<HistoryModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<HistoryModel>>(_uow.HistoryRepository.GetAllWithDetails());
            return res;
        }

        /// <summary>
        /// Returns histories made by corresponding employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of histories made by employee</returns>
        public IEnumerable<HistoryModel> GetAllByEmployeeId(int id)
        {
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => p.Assignment.EmployeeId == id).ToList();
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        /// <summary>
        /// Returns all histories from all projects where employee works.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<HistoryModel> GetAllByEmployeeProjects(int employeeId)
        {
            var projects = _uow.ProjectRepository.GetAllWithDetails().Where(p => p.Assignments.Select(p => p.EmployeeId).Contains(employeeId)).Select(p => p.Id);
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => projects.Contains(p.Assignment.Project.Id));
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        /// <summary>
        /// Returns all histories from projets made by corresponding manager.
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IEnumerable<HistoryModel> GetAllByManagerProjects(int managerId)
        {
            var projects = _uow.ProjectRepository.GetAllWithDetails().Where(p => p.ManagerId == managerId).Select(p => p.Id).ToList();
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => projects.Contains(p.Assignment.Project.Id));
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        /// <summary>
        /// Returns all histories from corresponding projects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<HistoryModel> GetAllByProjectId(int id)
        {
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => p.Assignment.ProjectID == id).ToList();
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        /// <summary>
        /// Returns history with corresponding Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>History with corresponding Id</returns>
        public async Task<HistoryModel> GetByIdAsync(int id)
        {
            var res = await _uow.HistoryRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<HistoryModel>(res);
        }

        /// <summary>
        /// Changes history record with corresponding Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(HistoryModel model)
        {
            _uow.HistoryRepository.Update(_mapper.Map<History>(model));
            await _uow.SaveAsync();
        }
    }
}
