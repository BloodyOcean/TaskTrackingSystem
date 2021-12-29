using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Administration
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
