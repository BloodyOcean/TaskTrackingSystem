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
    public class AssignmentStatusService : IAssignmentStatusService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public AssignmentStatusService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        /// <summary>
        /// Adds new status to db
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(AssignmentStatusModel model)
        {
            var element = _mapper.Map<AssignmentStatus>(model);
            var l = _uow.AssignmentStatusRepository.FindAll().First(p => p.Status == model.Status);

            if (l == null)
            {
                await _uow.AssignmentStatusRepository.AddAsync(element);
                await _uow.SaveAsync();
            }
            else
            {
                throw new System.Exception($"Status {model.Status} already exist.");
            }
        }

        /// <summary>
        /// Removes status with id from db
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.AssignmentStatusRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        /// <summary>
        /// Returns all status records from db
        /// </summary>
        /// <returns>Sequence of projects</returns>
        public IEnumerable<AssignmentStatusModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<AssignmentStatusModel>>(_uow.AssignmentStatusRepository.FindAll());
            return res;
        }

        /// <summary>
        /// Returns status with corresponding Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status with corresponding Id</returns>
        public async Task<AssignmentStatusModel> GetByIdAsync(int id)
        {
            var res = await _uow.AssignmentStatusRepository.GetByIdAsync(id);
            return _mapper.Map<AssignmentStatusModel>(res);
        }

        /// <summary>
        /// Changes status record with corresponding Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(AssignmentStatusModel model)
        {
            _uow.AssignmentStatusRepository.Update(_mapper.Map<AssignmentStatus>(model));
            await _uow.SaveAsync();
        }
    }
}
