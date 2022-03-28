using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System;

namespace SqlFunction
{
    public static class AddCourse
    {
        [FunctionName("AddCourse")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Course data = JsonConvert.DeserializeObject<Course>(requestBody);

            string connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            string statement = "INSERT INTO Course(CourseId, CourseName, Rating) VALUES (@param1,@param2,@param3)";
            using (var dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand(statement, dbConnection))
                {
                    cmd.Parameters.Add("@param1",System.Data.SqlDbType.Int).Value = data.CourseId;
                    cmd.Parameters.Add("@param2",System.Data.SqlDbType.VarChar).Value = data.CourseName;
                    cmd.Parameters.Add("@param3",System.Data.SqlDbType.Decimal).Value = data.Rating;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            return new OkObjectResult("Course Added");
        }
    }
}
