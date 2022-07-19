using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public interface ICourseController
    {
        IActionResult AddCourse(Course course);
        IActionResult GetCourse(string id);
        IActionResult GetCourses();
    }
}