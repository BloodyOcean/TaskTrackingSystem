using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class CompletionPercentage
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public double Percentage { get; set; }
    }
}
