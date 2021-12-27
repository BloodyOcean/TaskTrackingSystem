using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
