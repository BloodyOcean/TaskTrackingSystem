﻿using DAL.Interfaces;
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

        public readonly IEmployeeRepository _employeeRepository; 

        public readonly IHistoryRepository _historyRepository; 

        public readonly IProjectRepository _projectRepository;

        public UnitOfWork(TaskTrackingDbContext context,
            IAssignmentRepository assignmentRepository,
            IEmployeeRepository employeeRepository,
            IHistoryRepository historyRepository,
            IProjectRepository projectRepository)
        {
            _assignmentRepository = assignmentRepository;
            _employeeRepository = employeeRepository;
            _historyRepository = historyRepository;
            _projectRepository = projectRepository;

            this._db = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public IAssignmentRepository AssignmentRepository => _assignmentRepository ?? throw new ArgumentNullException(nameof(_assignmentRepository));
        public IEmployeeRepository EmployeeRepository => _employeeRepository ?? throw new ArgumentNullException(nameof(_employeeRepository));
        public IHistoryRepository HistoryRepository => _historyRepository ?? throw new ArgumentNullException(nameof(_historyRepository));
        public IProjectRepository ProjectRepository => _projectRepository ?? throw new ArgumentNullException(nameof(_projectRepository));

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}