using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class AssignmentModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public int? ProjectID { get; set; }
        public int? AssignmentStatusId { get; set; }
        public int? EmployeeId { get; set; }
        public ICollection<int> HistoryIds { get; set; }
    }
}
