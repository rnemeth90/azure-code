using Sqlapp.Interfaces;
using Sqlapp.Models;
using Sqlapp.Services;
using StackExchange.Redis;
using System;

namespace Sqlapp.Factories
{
    public static class CourseFactory
    {
        private static Lazy<IConnectionMultiplexer> CreateConnection(string connectionString)
        {
            return new Lazy<IConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(connectionString);
            });
        }

        public static ICourse CreateCourse()
        {
            return new Course();
        }

        public static ICourseService CreateCourseService(string connectionString)
        {
            return new CourseService(connectionString);
        }
    }
}
