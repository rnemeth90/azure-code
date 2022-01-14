using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp_HttpTrigger
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public Decimal Rating { get; set; }
    }
}
