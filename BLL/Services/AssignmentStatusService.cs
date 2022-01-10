using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Enitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task AddAsync(AssignmentStatusModel model)
        {
            var element = _mapper.Map<AssignmentStatus>(model);
            await _uow.AssignmentStatusRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.AssignmentStatusRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public IEnumerable<AssignmentStatusModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<AssignmentStatusModel>>(_uow.AssignmentStatusRepository.FindAll());
            return res;
        }

        public async Task<AssignmentStatusModel> GetByIdAsync(int id)
        {
            var res = await _uow.AssignmentStatusRepository.GetByIdAsync(id);
            return _mapper.Map<AssignmentStatusModel>(res);
        }

        public async Task UpdateAsync(AssignmentStatusModel model)
        {
            _uow.AssignmentStatusRepository.Update(_mapper.Map<AssignmentStatus>(model));
            await _uow.SaveAsync();
        }
    }
}
