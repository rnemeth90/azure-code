namespace Sqlapp.Interfaces
{
    public interface ICourse
    {
        int CourseID { get; set; }
        string CourseName { get; set; }
        decimal Rating { get; set; }
    }
}