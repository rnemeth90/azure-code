using Sqlapp.Interfaces;
using Sqlapp.Models;
using System.Collections.Generic;

namespace Sqlapp.Interfaces
{
    public interface ICourseService
    {
        ICourse GetCourse(int id);
        IEnumerable<ICourse> GetCourses();
        void UpdateCourse(int id, Course c);
        void CreateCourse(Course course);
    }
}