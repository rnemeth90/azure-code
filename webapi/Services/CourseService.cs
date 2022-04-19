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
        private static string _connectionString = @"DefaultEndpointsProtocol=https;AccountName=cs21003200044ebe600;AccountKey=AHcAUuTVXoogPpMkUZ/YBrmqrUB59hlh5IeBDRRsl2ONTY45C+vljecHkVnV4Sh3t1Npi5QxV7mZXIUVgfm39g==;EndpointSuffix=core.windows.net";
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
            var reader = new StreamReader (response.Value.Content); 

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
    }
}
