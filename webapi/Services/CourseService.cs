using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services
{
  public class CourseService
  {
    private static string _connectionString = @"";
    private static string _containerName = "data";
    private static string _blobName = "Courses.json";
    public class CourseService
    {
      private static string _connectionString = @"";
      private static string _containerName = "data";
      private static string _blobName = "Courses.json";

      internal IEnumerable<Course> GetCourses()
      {
        BlobServiceClient serviceClient = new BlobServiceClient(_connectionString);
        BlobContainerClient blobContainerClient = serviceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(_blobName);

        var response = blobClient.Download();
        var reader = new StreamReader(response.Value.Content);
        return JsonSerializer.Deserialize<Course[]>(reader.ReadToEnd());
      }

      internal Course GetCourse(string id)
      {
        IEnumerable<Course> courses = this.GetCourses();
        return courses.FirstOrDefault(c => c.CourseId == id);
      }

      internal void AddCourse(Course course)
      {
        List<Course> courses;
        BlobServiceClient serviceClient = new BlobServiceClient(_connectionString);
        BlobContainerClient blobContainerClient = serviceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(_blobName);

        var response = blobClient.Download();
        var reader = new StreamReader(response.Value.Content);

        courses = JsonSerializer.Deserialize<List<Course>>(reader.ReadToEnd());
        courses.Add(course);

        var output = JsonSerializer.Serialize(courses, new JsonSerializerOptions
        {
          WriteIndented = true,
        });

        var content = Encoding.UTF8.GetBytes(output);
        using (var ms = new MemoryStream(content))
        {
          blobClient.Upload(ms);
        }
      }

      internal void CreateCourses()
      {
        List<Course> courses = new List<Course>
            {
                new Course() { CourseId = 0, CourseName = "Course1", Rating=5},
                new Course() { CourseId = 1, CourseName = "Course2", Rating=1},
                new Course() { CourseId = 2, CourseName = "Course3", Rating=4},
                new Course() { CourseId = 3, CourseName = "Course4", Rating=7},
                new Course() { CourseId = 4, CourseName = "Course5", Rating=9},
                new Course() { CourseId = 5, CourseName = "Course6", Rating=3},
                new Course() { CourseId = 6, CourseName = "Course7", Rating=3}
            };

        var highlyRatedCourses = courses.Where(c => c.Rating > 7);
        var lowlyRatedCourses = courses.Where(c => c.Rating < 3);

        foreach (var item in highlyRatedCourses)
        {
          System.Console.WriteLine(item.CourseName);
        }
      }
    }
  }
