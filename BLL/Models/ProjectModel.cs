using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime ClosureDate { get; set; }
        [Required]
        public bool Status { get; set; }
        public int? ManagerId { get; set; }
        public ICollection<int> AssignmentIds { get; set; }
    }
}
