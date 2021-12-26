using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public bool Status { get; set; } 
        public int? ManagerId { get; set; }
        public ICollection<int> AssignmentIds { get; set; }
    }
}
