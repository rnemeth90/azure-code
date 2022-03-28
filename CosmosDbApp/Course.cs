using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbApp
{
    internal class Course
    {
        public string Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Rating { get; set; }
    }
}
