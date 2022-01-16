using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime ChangeDate { get; set; }
        [Required]
        public int? AssignmentId { get; set; }
        public string Comment { get; set; }
    }
}
