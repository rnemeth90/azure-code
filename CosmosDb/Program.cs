using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDb
{
    internal class Program
    {
        private static readonly string _connectionString = "AccountEndpoint=https://test-db-us-e-cosmos-01.documents.azure.com:443/;AccountKey=pRFUyKsRO6RD5Nj2yYy0nzG5UlRlBsr3lOK6khVl5ioRAHyrwgUqbfDOkV1U8ELrqyac2SkRmGNgPr9VONZkng==;";
        private static readonly string _databaseName = "appdb";
        private static readonly string _containerName = "course";
        private static readonly string _partitionKey = "/courseid";

        static void Main(string[] args)
        {
            CosmosClient client = new CosmosClient(_connectionString, new CosmosClientOptions() { AllowBulkExecution = true });
            //client.CreateDatabaseAsync(_databaseName).GetAwaiter().GetResult();
            //Database _database = client.GetDatabase(_databaseName);
            //_database.CreateContainerAsync(_containerName, _partitionKey).GetAwaiter().GetResult();
            //List<Course> courses = new List<Course>
            //{
            //   new Course() { id = "1", courseid = "C00010", CourseName = "AZ-204", Rating = 4.5m },
            //   new Course() { id = "2", courseid = "C00020", CourseName = "AZ-300", Rating = 4.5m },
            //   new Course() { id = "3", courseid = "C00030", CourseName = "AZ-301", Rating = 4.5m },
            //   new Course() { id = "4", courseid = "C00040", CourseName = "AZ-400", Rating = 4.5m },
            //   new Course() { id = "5", courseid = "C00050", CourseName = "AZ-500", Rating = 4.5m },
            //   new Course() { id = "6", courseid = "C00060", CourseName = "AZ-700", Rating = 4.5m },
            //   new Course() { id = "7", courseid = "C00070", CourseName = "AZ-900", Rating = 4.5m }
            //};


            Container container = client.GetContainer(_databaseName, _containerName);
            string output = container.Scripts.ExecuteStoredProcedureAsync<string>("demo", new PartitionKey(string.Empty), null).GetAwaiter().GetResult();
            Console.WriteLine(output);
            //List<Task> tasks = new List<Task>();

            //foreach (var course in courses)
            //{
            //    tasks.Add(container.CreateItemAsync<Course>(course, new PartitionKey(course.courseid)));
            //}
            //Task.WhenAll(tasks).GetAwaiter().GetResult();


            //string query = "SELECT * FROM c WHERE c.courseid='C00010'";
            //QueryDefinition definition = new QueryDefinition(query);
            //FeedIterator<Course> feedIterator = container.GetItemQueryIterator<Course>(definition);

            //while (feedIterator.HasMoreResults)
            //{
            //    FeedResponse<Course> response = feedIterator.ReadNextAsync().GetAwaiter().GetResult();
            //    foreach (var course in response)
            //    {
            //        Console.WriteLine($"Found course {course.id}");
            //    }
            //}

        }
    }
}
