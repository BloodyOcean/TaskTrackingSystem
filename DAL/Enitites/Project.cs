using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Enitites
{
    public class Project : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public bool Status { get; set; } // Open/Closed
        public int? ManagerId { get; set; }

        // Navigation properties.
        public ICollection<Assignment> Assignments { get; set; }

    }
}
