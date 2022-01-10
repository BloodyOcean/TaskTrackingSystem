using DAL.Enitites;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private TaskTrackingDbContext _db;
        public AssignmentRepository(TaskTrackingDbContext context)
        {
            this._db = context;
        }
        public async Task AddAsync(Assignment entity)
        {
            await _db.Assignments.AddAsync(entity);
        }

        public void Delete(Assignment entity)
        {
            var index = _db.Assignments.Find(entity);
            if (index != null)
            {
                _db.Assignments.Remove(entity);
            }
        }

        public void DeleteAssignmentsByEmployeeId(int id)
        {
            var assignments = _db.Assignments.Where(p => p.EmployeeId == id);
            foreach (var item in assignments)
            {
                _db.Assignments.Remove(item);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var en = await _db.Assignments.SingleOrDefaultAsync(p => p.Id == id);
            if (en != null)
            {
                _db.Assignments.Remove(en);
            }
        }

        public IQueryable<Assignment> FindAll()
        {
            return _db.Assignments.AsQueryable();
        }

        public IQueryable<Assignment> GetAllWithDetails()
        {
            return _db.Assignments.Include(p => p.Project).Include(p => p.Histories);
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            var sources = await _db.Assignments
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return sources;
        }

        public async Task<Assignment> GetByIdWithDetailsAsync(int id)
        {
            return await _db.Assignments
                .Include(p => p.Project)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Assignment entity)
        {
            _db.Assignments.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
