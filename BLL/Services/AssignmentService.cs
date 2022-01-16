using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Enitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public AssignmentService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        /// <summary>
        /// Adds new task to db
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(AssignmentModel model)
        {
            var element = _mapper.Map<Assignment>(model);
            await _uow.AssignmentRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Removes task with id from db
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.AssignmentRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Returns all task records from db
        /// </summary>
        /// <returns>Sequence of projects</returns>
        public IEnumerable<AssignmentModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<AssignmentModel>>(_uow.AssignmentRepository.GetAllWithDetails());
            return res;
        }

        /// <summary>
        /// Returns list of tasks made by corresponding employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<AssignmentModel> GetAssignmentsByEmployee(int id)
        {
            return _mapper.Map<IEnumerable<AssignmentModel>>(_uow.AssignmentRepository.GetAllWithDetails().Where(p => p.EmployeeId == id).ToList());
        }

        /// <summary>
        /// Returns task with corresponding Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task with corresponding Id</returns>
        public async Task<AssignmentModel> GetByIdAsync(int id)
        {
            var res = await _uow.AssignmentRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<AssignmentModel>(res);
        }

        /// <summary>
        /// Returns Id of employee who has corresponding task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetEmployeeId(int id)
        {
            var res = _uow.AssignmentRepository.FindAll().First(p => p.Id == id);
            return (int)res.EmployeeId;
        }

        /// <summary>
        /// Removes all tasks made by corresponding employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAssignmentsByEmployeeId(int id)
        {
            _uow.AssignmentRepository.DeleteAssignmentsByEmployeeId(id);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Changes task record with corresponding Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(AssignmentModel model)
        {
            _uow.AssignmentRepository.Update(_mapper.Map<Assignment>(model));
            await _uow.SaveAsync();
        }
    }
}
