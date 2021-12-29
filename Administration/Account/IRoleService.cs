using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Account
{
    public interface IRoleService
    {
        Task AssignUserToRoles(AssignUserToRoles assignUserToRoles);
        Task CreateRole(string roleName);
        Task<IEnumerable<string>> GetRoles(ApplicationUser user);
        Task<IEnumerable<IdentityRole>> GetRoles();
        Task DeleteRole(string name);
    }
}
