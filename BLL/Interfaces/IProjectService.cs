using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService : ICrud<ProjectModel>
    {
        //IEnumerable<ProjectModel> GetByFilter(FilterSearchModel filterSearch);
        Task<ProjectModel> AssignManagerToProject(AssignManagerToProjectModel model);
        public IEnumerable<ProjectModel> GetProjectsByEmployee(int id);
    }
}
