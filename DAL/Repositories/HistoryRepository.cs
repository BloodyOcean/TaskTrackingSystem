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
    public class HistoryRepository : IHistoryRepository
    {
        private TaskTrackingDbContext _db;
        public HistoryRepository(TaskTrackingDbContext context)
        {
            this._db = context;
        }
        public async Task AddAsync(History entity)
        {
            await _db.Histories.AddAsync(entity);
        }

        public void Delete(History entity)
        {
            var index = _db.Histories.Find(entity);
            if (index != null)
            {
                _db.Histories.Remove(entity);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var en = await _db.Histories.SingleOrDefaultAsync(p => p.Id == id);
            _db.Histories.Remove(en);
        }

        public IQueryable<History> FindAll()
        {
            return _db.Histories.AsQueryable();
        }

        public IQueryable<History> GetAllWithDetails()
        {
            /*return _db.Histories
                .Include(p => p.Assignment)
                .Include(p => p.Employee);*/

            return _db.Histories.Include(p => p.Assignment);
        }

        public async Task<History> GetByIdAsync(int id)
        {
            var sources = await _db.Histories
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return sources;
        }

        public void Update(History entity)
        {
            _db.Histories.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
