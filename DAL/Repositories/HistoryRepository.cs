using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public class HistoryRepository : IHistoryRepository
    {
        public Task AddAsync(History entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(History entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<History> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<History> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<History> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(History entity)
        {
            throw new NotImplementedException();
        }
    }
}
