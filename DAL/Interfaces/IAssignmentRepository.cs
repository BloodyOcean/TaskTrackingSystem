using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        IQueryable<Assignment> GetAllWithDetails();

        Task<Assignment> GetByIdWithDetailsAsync(int id);
    }
}
