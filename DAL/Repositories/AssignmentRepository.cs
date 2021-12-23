using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public Task AddAsync(Assignment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Assignment entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Assignment> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Assignment> FindAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<Assignment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Assignment> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Assignment entity)
        {
            throw new NotImplementedException();
        }
    }
}
