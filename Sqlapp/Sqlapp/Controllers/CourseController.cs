﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly CourseService _course_service;
        private readonly IConfiguration _configuration;

        public CourseController(CourseService _svc,IConfiguration configuration)
        {
            _course_service = _svc;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            // Use the configuration class to get the connection string
            IEnumerable<Course> _course_list = _course_service.GetCourses(_configuration.GetConnectionString("SQLConnection"));
            return View(_course_list);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_course_service.GetCourse(id, _configuration.GetConnectionString("SQLConnection")));
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _course_service.UpdateCourse(course, _configuration.GetConnectionString("SQLConnection"));
            return RedirectToAction("Index");
        }
    }
}
