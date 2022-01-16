using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class AssignmentStatusModel
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
