using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAssignmentService : ICrud<AssignmentModel>
    {
        public IEnumerable<AssignmentModel> GetAssignmentsByEmployee(int id);
        public Task RemoveAssignmentsByEmployeeId(int id);
        public int GetEmployeeId(int id);
    }
}
