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
    public class ProjectRepository : IProjectRepository
    {
        private TaskTrackingDbContext _db;
        public ProjectRepository(TaskTrackingDbContext context)
        {
            this._db = context;
        }
        public async Task AddAsync(Project entity)
        {
            await _db.Projects.AddAsync(entity);
        }

        public void Delete(Project entity)
        {
            var index = _db.Projects.Find(entity);
            if (index != null)
            {
                _db.Projects.Remove(entity);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var en = await _db.Projects.SingleOrDefaultAsync(p => p.Id == id);
            _db.Projects.Remove(en);
        }

        public IQueryable<Project> FindAll()
        {
            return _db.Projects.AsQueryable();
        }

        public IQueryable<Project> GetAllWithDetails()
        {
            return _db.Projects
               .Include(p => p.Employee)
               .Include(p => p.Assignments);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var sources = await _db.Projects
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return sources;
        }

        public async Task<Project> GetByIdWithDetailsAsync(int id)
        {
            return await _db.Projects
                .Include(p => p.Employee)
                .Include(p => p.Assignments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Project entity)
        {
            _db.Projects.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
