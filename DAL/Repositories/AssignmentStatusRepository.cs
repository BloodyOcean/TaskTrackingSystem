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
    public class AssignmentStatusRepository : IAssignmentStatusRepository
    {
        private TaskTrackingDbContext _db;
        public AssignmentStatusRepository(TaskTrackingDbContext context)
        {
            this._db = context;
        }
        public async Task AddAsync(AssignmentStatus entity)
        {
            await _db.AssignmentStatuses.AddAsync(entity);
        }

        public void Delete(AssignmentStatus entity)
        {
            var index = _db.AssignmentStatuses.Find(entity);
            if (index != null)
            {
                _db.AssignmentStatuses.Remove(entity);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var en = await _db.AssignmentStatuses.SingleOrDefaultAsync(p => p.Id == id);
            if (en != null)
            {
                _db.AssignmentStatuses.Remove(en);
            }
        }

        public IQueryable<AssignmentStatus> FindAll()
        {
            return _db.AssignmentStatuses.AsQueryable();
        }

        public async Task<AssignmentStatus> GetByIdAsync(int id)
        {
            var sources = await _db.AssignmentStatuses
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return sources;
        }

        public void Update(AssignmentStatus entity)
        {
            _db.AssignmentStatuses.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
