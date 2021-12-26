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
    public class AssignmentService : IAssignmentService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public AssignmentService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }
        public async Task AddAsync(AssignmentModel model)
        {
            var element = _mapper.Map<Assignment>(model);
            await _uow.AssignmentRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.AssignmentRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public IEnumerable<AssignmentModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<AssignmentModel>>(_uow.ProjectRepository.FindAll());
            return res;
        }

        public async Task<AssignmentModel> GetByIdAsync(int id)
        {
            var res = await _uow.AssignmentRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<AssignmentModel>(res);
        }

        public async Task UpdateAsync(AssignmentModel model)
        {
            _uow.AssignmentRepository.Update(_mapper.Map<Assignment>(model));
            await _uow.SaveAsync();
        }
    }
}
