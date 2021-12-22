using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUnitOfWork
    {
        IAssignmentRepository AssignmentRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        IHistoryRepository HistoryRepository { get; }

        Task<int> SaveAsync();
    }
}
