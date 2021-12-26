using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<int> AssignmentIds { get; set; }
        public ICollection<int> HostoryIds { get; set; }
        public ICollection<int> ProjectIds { get; set; }
    }
}
