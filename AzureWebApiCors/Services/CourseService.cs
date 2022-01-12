using AzureWebApiCors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureWebApiCors.Services
{
    public class CourseService
    {
        public List<Course> course_lst;

        public CourseService()
        {
            course_lst = new List<Course>()
            {
                new Course() {CourseId=1,CourseName="AZ-204"},
                new Course() {CourseId=2,CourseName="AZ-303"},
                new Course() {CourseId=3,CourseName="AZ-304"}
            };
        }

        public IEnumerable<Course> GetCourses()
        {
            return (course_lst);
        }
    }
}
