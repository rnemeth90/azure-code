using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Linq;

namespace MSLearn_Storage
{
    internal class Program
    {
        internal static string connectionString = "";
        static BlobServiceClient blobServiceClient = new(connectionString);
        static AsyncPageable<BlobContainerItem> blobContainerClient = blobServiceClient.GetBlobContainersAsync();

        static async Task Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                await CreateContainer("my-container");
            }
            await GetContainers();
            ReadContainerPropertiesAsync(new BlobContainerClient(connectionString,);

            Console.ReadLine();
        }

        static async Task<List<string>> GetContainers()
        {
            List<string> containers = new();
            Console.WriteLine("Found these containers:");
            await foreach (var c in blobContainerClient)
            {
                containers.Add(c.ToString());
            }
            return containers;
        }

        static async Task CreateContainer(string containerName)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var name = string.Concat(containerName,"-",rand.Next());

            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(name);
            Console.WriteLine($"[Created container] {name}");
        }

        private static async Task ReadContainerPropertiesAsync(BlobContainerClient container)
        {
            try
            {
                // Fetch some container properties and write out their values.
                var properties = await container.GetPropertiesAsync();
                Console.WriteLine($"Properties for container {container.Uri}");
                Console.WriteLine($"Public access level: {properties.Value.PublicAccess}");
                Console.WriteLine($"Last modified time in UTC: {properties.Value.LastModified}");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
