using Microsoft.EntityFrameworkCore;
using Sqlapp.Models;

namespace Sqlapp.Interfaces
{
    public interface ICourseDbContext
    {
        DbSet<Course> Courses { get; set; }
    }
}