using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageUtils
{
    class BlobContainers
    {
        public static string Create(string connectionString, string containerName)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.CreateBlobContainer(containerName);

            return $"{containerName} created!";
        }

        public static List<BlobContainerItem> ListContainers(string connectionString)
        {
            List<BlobContainerItem> containers = new List<BlobContainerItem>();
            BlobServiceClient serviceClient = new BlobServiceClient(connectionString);

            foreach (var item in serviceClient.GetBlobContainers())
            {
                containers.Add(item);
            }
            return containers;  
        }
    }
}
