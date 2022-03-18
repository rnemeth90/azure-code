namespace Sqlapp.Interfaces
{
    public interface ICourse
    {
        int CourseID { get; set; }
        string CourseName { get; set; }
        string Description { get; set; }
        string Instructor { get; set; }
        decimal Rating { get; set; }
    }
}