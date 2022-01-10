using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskTrackingDbContext _db;

        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAssignmentStatusRepository _assignmentStatusRepository;
        public readonly IHistoryRepository _historyRepository; 
        public readonly IProjectRepository _projectRepository;

        public UnitOfWork(TaskTrackingDbContext context,
            IAssignmentRepository assignmentRepository,
            IHistoryRepository historyRepository,
            IAssignmentStatusRepository assignmentStatusRepository,
            IProjectRepository projectRepository)
        {
            _assignmentRepository = assignmentRepository;
            _historyRepository = historyRepository;
            _projectRepository = projectRepository;
            _assignmentStatusRepository = assignmentStatusRepository;

            this._db = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public IAssignmentRepository AssignmentRepository => _assignmentRepository ?? throw new ArgumentNullException(nameof(_assignmentRepository));
        public IHistoryRepository HistoryRepository => _historyRepository ?? throw new ArgumentNullException(nameof(_historyRepository));
        public IProjectRepository ProjectRepository => _projectRepository ?? throw new ArgumentNullException(nameof(_projectRepository));
        public IAssignmentStatusRepository AssignmentStatusRepository => _assignmentStatusRepository ?? throw new ArgumentNullException(nameof(_projectRepository));

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
