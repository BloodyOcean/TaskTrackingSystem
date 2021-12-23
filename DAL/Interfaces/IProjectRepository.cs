using DAL.Enitites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        IQueryable<Project> GetAllWithDetails();
        Task<Project> GetByIdWithDetailsAsync(int id);
    }
}
