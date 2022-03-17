using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sqlapp.Interfaces;
using Sqlapp.Models;
using Sqlapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sqlapp.Controllers
{
    public class CourseController : Controller
    {
        //private readonly IConfiguration _configuration;
        private ICourseService _courseService;

        public CourseController(IConfiguration configuration)
        {
            //_configuration = configuration;
            //var _courseService = Factory.CreateCourseService(_configuration.GetConnectionString("SQLConnection"));
        }

        public IActionResult Index()
        {
            // Use the configuration class to get the connection string
            IEnumerable<ICourse> _course_list = _courseService.GetCourses();
            return View(_course_list);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_courseService.GetCourse(id));
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _courseService.UpdateCourse(course);
            return RedirectToAction("Index");
        }
    }
}
