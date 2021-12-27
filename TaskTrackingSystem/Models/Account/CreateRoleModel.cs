using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.Models.Account
{
    public class CreateRoleModel
    {
        [Required, MinLength(5), MaxLength(20)]
        public string RoleName { get; set; }
    }
}
