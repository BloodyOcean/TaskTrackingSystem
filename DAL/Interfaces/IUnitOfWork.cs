using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAssignmentRepository AssignmentRepository { get; }

        IHistoryRepository HistoryRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IAssignmentStatusRepository AssignmentStatusRepository { get; }

        Task<int> SaveAsync();
    }
}
