using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IHistoryRepository : IRepository<History>
    {
        IQueryable<History> GetAllWithDetails();
        Task<History> GetByIdWithDetailsAsync(int id);
    }
}
