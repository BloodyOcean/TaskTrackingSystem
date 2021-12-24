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
    public class EmployeeRepository : IEmployeeRepository
    {
        private TaskTrackingDbContext _db;
        public EmployeeRepository(TaskTrackingDbContext context)
        {
            this._db = context;
        }

        public async Task AddAsync(Employee entity)
        {
            await _db.AddAsync(entity);
        }

        public void Delete(Employee entity)
        {
            var index = _db.Employees.Find(entity);
            if (index != null)
            {
                _db.Employees.Remove(entity);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var en = await _db.Employees.SingleOrDefaultAsync(p => p.Id == id);
            _db.Employees.Remove(en);
        }

        public IQueryable<Employee> FindAll()
        {
            return _db.Employees.AsQueryable();
        }

        public IQueryable<Employee> GetAllWithDetails()
        {
            return _db.Employees.Include(p => p.Projects).Include(p => p.Assignments);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var sources = await _db.Employees
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return sources;
        }

        public async Task<Employee> GetByIdWithDetailsAsync(int id)
        {
            return await _db.Employees
                .Include(p => p.Projects)
                .Include(p => p.Assignments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Employee entity)
        {
            _db.Employees.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
