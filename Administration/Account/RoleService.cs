using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Account
{
    /// <summary>
    /// This class is used for managing roles
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// takes email and string array with role names and add this roles to user
        /// </summary>
        /// <param name="assignUserToRoles"></param>
        /// <returns></returns>
        public async Task AssignUserToRoles(AssignUserToRoles assignUserToRoles)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == assignUserToRoles.Email);
            var roles = _roleManager.Roles
                .ToList()
                .Where(r => assignUserToRoles.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles); 

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }

        /// <summary>
        /// Takes string with role name and creates new role with same name.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task CreateRole(string roleName)
        {
            // If roles already exist.
            var res = await _roleManager.FindByNameAsync(roleName);
            if (res != null)
            {
                throw new System.Exception($"Role {roleName} already exist.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new System.Exception($"Role could not be created: {roleName}.");
            }
        }

        /// <summary>
        /// Removes role with corresponding name from db
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task DeleteRole(string name)
        {
            var res = await _roleManager.FindByNameAsync(name);
      
            if (res != null)
            {
                await _roleManager.DeleteAsync(res);
            }
        }

        /// <summary>
        /// Returns all roles from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRoles(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }
    }
}
