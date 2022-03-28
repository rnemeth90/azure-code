using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Text;
using Sqlapp.Interfaces;
using Sqlapp.Factories;
using Sqlapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Sqlapp.Models;

namespace Sqlapp.Services
{
    public class CourseService : ICourseService
    {
        private CourseDbContext _dbContext;

        public CourseService(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ICourse> GetCourses()
        {
            return _dbContext.Courses;
        }

        public void UpdateCourse(int id, Course c)
        {
            var course = _dbContext.Courses.Find(id);
            course.Rating = c.Rating;
            course.Description = c.Description;
            course.Instructor = c.Instructor;
            course.CourseName = c.CourseName;
            _dbContext.SaveChanges();
        }

        public ICourse GetCourse(int id)
        {
            return _dbContext.Courses.Find(id);
        }
    }
}

    

