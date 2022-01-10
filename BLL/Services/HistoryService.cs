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
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => p.Assignment.EmployeeId == id).ToList();
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        public IEnumerable<HistoryModel> GetAllByEmployeeProjects(int employeeId)
        {
            var projects = _uow.ProjectRepository.GetAllWithDetails().Where(p => p.Assignments.Select(p => p.EmployeeId).Contains(employeeId)).Select(p => p.Id);
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => projects.Contains(p.Assignment.Project.Id));
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        public IEnumerable<HistoryModel> GetAllByManagerProjects(int managerId)
        {
            var projects = _uow.ProjectRepository.GetAllWithDetails().Where(p => p.ManagerId == managerId).Select(p => p.Id).ToList();
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => projects.Contains(p.Assignment.Project.Id));
            return _mapper.Map<IEnumerable<HistoryModel>>(res);
        }

        public IEnumerable<HistoryModel> GetAllByProjectId(int id)
        {
            var res = _uow.HistoryRepository.GetAllWithDetails().Where(p => p.Assignment.ProjectID == id).ToList();
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
