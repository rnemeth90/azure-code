using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("/api/course")]

    public class CourseController : ControllerBase
    {
        internal readonly CourseService _courseService;

        public CourseController(CourseService service)
        {
            _courseService = service;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok(_courseService.GetCourses());
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(string id)
        { 
            return Ok(_courseService.GetCourse(id));
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            _courseService.AddCourse(course);
            return Ok();    
        }
    }
}
