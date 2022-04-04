using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CosmosApp01
{
    internal class Program
    {
        internal static string _connectionString = @"AccountEndpoint=https://azrtncosmosdb01.documents.azure.com:443/;AccountKey=SkdfDyMkRogRCZFK9ISDMQ6wpuofiTQIzbZTF1lqoS96oS1wsO45uzdLVu7itGvaxomiKjJO7xfWtic5sAkSgw==;";
        internal static string _databaseName = "Humans";
        internal static string _containerName = "People";
        internal static Logger _logger = new Logger();

        static void Main(string[] args)
        {
            _logger.LogInfo("Connecting to Cosmos and creating database...");
            CosmosClient cosmosClient = new CosmosClient(_connectionString);
            cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);


            _logger.LogInfo("Getting handle on database and creating container...");
            var db = cosmosClient.GetDatabase(_databaseName);
            db.CreateContainerIfNotExistsAsync(new ContainerProperties(_containerName,"/Name"));
            var container = db.GetContainer(_containerName);

            AddItemsToContainerAsync(container).GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private static async Task AddItemsToContainerAsync(Container container)
        {
            List<Person> People = new List<Person>
            {
                //new Person { id="1", Name="Ryan Nemeth",FirstName="Ryan",LastName="Nemeth"},
                //new Person { id="2", Name="Jessica Nemeth",FirstName="Jessica",LastName="Nemeth"},
                new Person { id="3", Name="Emma Nemeth",FirstName="Emma",LastName="Nemeth"}
            };

            foreach (var p in People)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen".
                ItemResponse<Person> response = await container.CreateItemAsync<Person>(p, new PartitionKey(p.Name));
                // Note that after creating the item, we can access the body of the item with the Resource property of the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", response.Resource.id, response.RequestCharge);
            }
        }
    }
}
