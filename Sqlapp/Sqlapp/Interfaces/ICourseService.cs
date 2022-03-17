using Sqlapp.Interfaces;
using System.Collections.Generic;

namespace Sqlapp.Interfaces
{
    public interface ICourseService
    {
        ICourse GetCourse(string id);
        IEnumerable<ICourse> GetCourses();
        void UpdateCourse(ICourse p_course);
    }
}