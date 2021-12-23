using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Enitites
{
    public class Assignment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public int? ProjectID { get; set; }
        public int? AssignmentStatusId { get; set; }
        public int? EmployeeId { get; set; }

        // Navigation properties.
        public Project Project { get; set; }
        public Employee Employee { get; set; }
        public ICollection<History> Histories { get; set; }
        public AssignmentStatus AssignmentStatus { get; set; }
    }
}
