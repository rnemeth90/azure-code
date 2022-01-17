using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp_CosmosDb_ChangeFeed
{
    internal class Course
    {
        public string id { get; set; }
        public string courseid { get; set; }
        public string coursename { get; set; }
        public decimal rating { get; set; } 
    }
}
