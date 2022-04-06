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
using StackExchange.Redis;
using System.Text.Json;

namespace Sqlapp.Services
{
    public class CourseService : ICourseService
    {
        private CourseDbContext _dbContext;
        private Lazy<ConnectionMultiplexer> _connection;

        public ConnectionMultiplexer Connection { get => _connection.Value; }

        public CourseService(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
            _connection = CreateRedisConnection();
        }

        internal Lazy<ConnectionMultiplexer> CreateRedisConnection()
        {
            var connectionString = "azrtnrediscache.redis.cache.windows.net:6380,password=v1QEw1jHdI2h41mz1MaA0j1XT4tTG4J56AzCaKETvU4=,ssl=True,abortConnect=False";
            return new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(connectionString);
            });
        }

        public IEnumerable<ICourse> GetCourses()
        {
            List<Course> courses = _dbContext.Courses.ToList();
            IDatabase cache = Connection.GetDatabase();
            if (cache.KeyExists("Course"))
            {
                var list = JsonSerializer.Deserialize<List<Course>>(cache.StringGet("Course"));
                return list;
            }
            else
            {
                cache.StringSet("Course",JsonSerializer.Serialize<List<Course>>(courses));
                return courses;
            }
        }

        public void CreateCourse(Course c)
        {
            _dbContext.Courses.Add(c);
            _dbContext.SaveChanges();
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

    

