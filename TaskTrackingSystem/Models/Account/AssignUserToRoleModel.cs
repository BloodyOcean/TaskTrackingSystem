using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.Models.Account
{
    public class AssignUserToRoleModel
    {
        [Required]
        public string Email { get; set; }
        [Required, MinLength(1)]
        public string[] Roles { get; set; }
    }
}
