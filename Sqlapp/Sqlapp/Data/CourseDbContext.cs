using Microsoft.EntityFrameworkCore;
using Sqlapp.Interfaces;
using Sqlapp.Models;

namespace Sqlapp.Data
{
    public class CourseDbContext : DbContext, ICourseDbContext
    {
        public DbSet<Course> Courses { get; set; }

        public CourseDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
