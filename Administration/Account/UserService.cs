using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Account
{
    /// <summary>
    /// This class is used for managing accounts
    /// </summary>
    public sealed class UserService : IUserService
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task DeleteAccountByEmail(string email)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            await _userManager.DeleteAsync(user);
        }

        /// <summary>
        /// Removes account from db with corresponding UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAccountByUserId(int id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserId == id);
            await _userManager.DeleteAsync(user);
        }

        /// <summary>
        /// Returns all accounts from db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users;
        }

        public async Task<int> GetCurrentUserIdAsync(ClaimsPrincipal currentUser)
        {
            var u = await _userManager.GetUserAsync(currentUser); // Get user id:
            return u.UserId;
        }

        /// <summary>
        /// Checks email and password and if user exist, returns this user
        /// </summary>
        /// <param name="logon"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> Logon(Logon logon)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == logon.Email);
            if (user is null)
            {
                throw new System.Exception($"User not found: '{logon.Email}'.");
            }

            return await _userManager.CheckPasswordAsync(user, logon.Password) ? user : null;
        }

        /// <summary>
        /// Creates new user in db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Register(Register user)
        {

            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email,
                Name = user.Name

            }, user.Password);

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }
    }
}
