using Sqlapp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sqlapp.Models
{
    public class Course : ICourse
    {
        // This class represents the structure of our data
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public decimal Rating { get; set; }
    }
}
