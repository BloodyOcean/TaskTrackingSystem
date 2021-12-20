using DAL.Enitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Enitites
{
    public class AssignmentStatus : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }

        // Navigation properties.
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
