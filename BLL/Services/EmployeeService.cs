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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;

        public EmployeeService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        public async Task AddAsync(EmployeeModel model)
        {
            var element = _mapper.Map<Employee>(model);
            await _uow.EmployeeRepository.AddAsync(element);
            await _uow.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _uow.EmployeeRepository.DeleteByIdAsync(modelId);
            await _uow.SaveAsync();
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            var res = _mapper.Map<IEnumerable<EmployeeModel>>(_uow.EmployeeRepository.FindAll());
            return res;
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            var res = await _uow.EmployeeRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<EmployeeModel>(res);
        }

        public async Task UpdateAsync(EmployeeModel model)
        {
            _uow.EmployeeRepository.Update(_mapper.Map<Employee>(model));
            await _uow.SaveAsync();
        }
    }
}
