using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.Models.Account
{
    public class LogonModel
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
