using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public interface IHistoryRepository : IRepository<History>
    {
        IQueryable<History> GetAllWithDetails();
    }
}
