namespace webapi.Models
{
    public class Course : ICourse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Rating { get; set; }
    }
}
