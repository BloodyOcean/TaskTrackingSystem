using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAssignmentRepository AssignmentRepository => throw new NotImplementedException();

        public IEmployeeRepository EmployeeRepository => throw new NotImplementedException();

        public IHistoryRepository HistoryRepository => throw new NotImplementedException();

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
