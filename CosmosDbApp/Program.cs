using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDbApp
{
    internal class Program
    {
        private static readonly string connectionString = @"AccountEndpoint=https://azrtncosmosdb01.documents.azure.com:443/;AccountKey=Shp9EwQqNcmMWJF73gPnVzYBGKFTvSyHuVNXnUvf2ZjEvYpG1zh1onJUsDa8FeR7Oo1zLtNyKaGTZNJwlLUrLw==;";
        private static readonly string databaseName = "appdb";
        private static readonly string containerName = "course";
        private static readonly string partitionKey = "/courseid";

        static void Main(string[] args)
        {
            CosmosClient dbClient = new(connectionString, new CosmosClientOptions() { AllowBulkExecution=true }) ;
            //CreateDatabase(dbClient, databaseName);
            //CreateContainer(dbClient,databaseName,containerName,partitionKey);
            //Console.ReadKey();

            List<Course> courses = new List<Course>
            {
                new Course { CourseId="1", CourseName="Az-204", Id="1", Rating=4.44m},
                new Course { CourseId="2", CourseName="Az-300", Id="2", Rating=4.44m},
                new Course { CourseId="3", CourseName="Az-301", Id="3", Rating=4.44m},
                new Course { CourseId="4", CourseName="Az-400", Id="4", Rating=4.44m}
            };

            var container = dbClient.GetContainer(databaseName, containerName);
            List<Task> tasks = new List<Task>();

            foreach (var course in courses)
            {
                tasks.Add(container.CreateItemAsync<Course>(course, new PartitionKey(course.CourseId)));
                Console.WriteLine($"Course {course.CourseName} has been added. ");
            }

            Task.WhenAll(tasks).GetAwaiter().GetResult();
            Console.WriteLine("Bulk insert completed. ");
        }

        internal static void CreateDatabase(CosmosClient client, string databaseName)
        { 
            client.CreateDatabaseAsync(databaseName).GetAwaiter().GetResult();
        }

        internal static void CreateContainer(CosmosClient client, string databaseName, 
                                     string containerName, string partitionKey)
        {
            Database db = client.GetDatabase(databaseName);
            db.CreateContainerAsync(containerName, partitionKey).GetAwaiter().GetResult();
        }

    }
}
