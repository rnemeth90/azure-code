using Sqlapp.Interfaces;
using System.Collections.Generic;

namespace Sqlapp.Interfaces
{
    public interface ICourseService
    {
        ICourse GetCourse(int id);
        IEnumerable<ICourse> GetCourses();
        void UpdateCourse(ICourse c);
    }
}