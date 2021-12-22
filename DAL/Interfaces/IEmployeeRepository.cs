using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IQueryable<History> GetAllWithDetails();
        Task<Assignment> GetByIdWithDetailsAsync(int id);
    }
}
