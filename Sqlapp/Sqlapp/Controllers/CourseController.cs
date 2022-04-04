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
        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ICourse> _course_list = _courseService.GetCourses();
            return View(_course_list);
        }

        public IActionResult Create([FromBody]Course course)
        {
            _courseService.CreateCourse(course);
            var id = course.CourseID;
            return View(_courseService.GetCourse(id));
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(_courseService.GetCourse(id));
        }

        public IActionResult Details(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            return View(_courseService.GetCourse(id));
        }

        //[HttpPost]
        //public IActionResult Edit(int id, Course course)
        //{
        //    _courseService.UpdateCourse(id, course);
        //    return RedirectToAction("Index");
        //}
    }
}
