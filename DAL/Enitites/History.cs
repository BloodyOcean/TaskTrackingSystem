using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Enitites
{
    public class History : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        //public int? UpdatedBy { get; set; }
        public int? AssignmentId { get; set; }
        public string Comment { get; set; }

        // Navigation properties.
        public virtual Assignment Assignment { get; set; }
    }
}
