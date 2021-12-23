using DAL.Enitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IManagerRepository : IRepository<Manager>
    {
        IQueryable<Manager> GetAllWithDetails();
        Task<Manager> GetByIdWithDetailsAsync(int id);
    }
}
