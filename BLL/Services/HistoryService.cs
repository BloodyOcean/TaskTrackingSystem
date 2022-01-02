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
    public class HistoryService : IHistoryService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public HistoryService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }
        public async Task AddAsync(HistoryModel model)
        {
            var element = _mapper.Map<History>(model);
            await _uow.HistoryRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.HistoryRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public IEnumerable<HistoryModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<HistoryModel>>(_uow.HistoryRepository.GetAllWithDetails());
            return res;
        }

        public IEnumerable<HistoryModel> GetAllByEmployeeId(int id)
        {
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => p.UpdatedBy == id).ToList();
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        public async Task<HistoryModel> GetByIdAsync(int id)
        {
            var res = await _uow.HistoryRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<HistoryModel>(res);
        }

        public async Task UpdateAsync(HistoryModel model)
        {
            _uow.HistoryRepository.Update(_mapper.Map<History>(model));
            await _uow.SaveAsync();
        }
    }
}
