using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Account
{
    public interface IUserService
    {
        Task Register(Register user);
        Task<ApplicationUser> Logon(Logon logon);
        IEnumerable<ApplicationUser> GetAll();
    }
}
