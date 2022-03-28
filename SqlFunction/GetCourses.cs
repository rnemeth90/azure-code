using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlFunction
{
    public static class GetCourses
    {
        [FunctionName("GetCourses")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<Course> courses = new List<Course>();
            string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            string statement = "SELECT CourseId, CourseName, Rating from Course";

            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                SqlCommand command = new SqlCommand(statement, dbConnection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Course course = new Course()
                        {
                            CourseId = reader.GetInt32(0),
                            CourseName = reader.GetString(1),
                            Rating = reader.GetDecimal(2)
                        };
                        courses.Add(course);
                    }
                }
            }
            return new OkObjectResult(JsonConvert.SerializeObject(courses));
        }
    }
}
