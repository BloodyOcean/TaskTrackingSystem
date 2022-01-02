using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? AssignmentId { get; set; }
        public string Comment { get; set; }
    }
}
