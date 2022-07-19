namespace webapi.Models
{
    public interface ICourse
    {
        string CourseId { get; set; }
        string CourseName { get; set; }
        decimal Rating { get; set; }
    }
}