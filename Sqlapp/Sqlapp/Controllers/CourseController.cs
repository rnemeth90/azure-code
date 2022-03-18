﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(_courseService.GetCourse(id));
        }

        public IActionResult Details(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            return View(_courseService.GetCourse(id));
        }

        [HttpPost]
        public IActionResult Edit(ICourse course)
        {
            _courseService.UpdateCourse(course);
            return RedirectToAction("Index");
        }
    }
}
