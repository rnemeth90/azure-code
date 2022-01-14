using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzureStorageUtils
{
    class Blob
    {
        public static string UploadBlob(string connectionString, string containerName, string blobPath, string blobName)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            blobClient.Upload(blobPath);


            return $"Uploaded {blobName}!";
        }

        // WIP
        public static List<string> UploadBlobs(string connectionString, string containerName)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            List<string> blobs = new List<string>();

            foreach (var item in containerClient.GetBlobs())
            {
                blobs.Add(item.Name);
            }
            return blobs;
        }

        public static string DownloadBlob(string connectionString, string containerName, string blobName, string localPath)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            blobClient.DownloadTo(localPath);

            return $"Downloaded {blobName}";   
        }

        public static IDictionary<string,string> GetMetadata(string connectionString, string containerName, string blobName)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            BlobProperties properties = blobClient.GetProperties();
            IDictionary<string,string> metadata = properties.Metadata;
            return properties.Metadata;
        }

        public static void SetMetadata(string connectionString, string containerName, string blobName,string key, string value)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            BlobProperties properties = blobClient.GetProperties();
            IDictionary<string, string> metadata = properties.Metadata;

            metadata.Add(key, value);
            blobClient.SetMetadata(metadata);
        }

        public static void WriteToBlob(string connectionString, string containerName, string blobName)
        {
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            MemoryStream memory = new MemoryStream();
            blobClient.DownloadTo(memory);

            // Read the blob
            //StreamReader reader = new StreamReader(memory);
            //Console.WriteLine(reader.ReadToEnd());

            StreamWriter writer = new StreamWriter(memory);
            writer.WriteLine("Hello World!");
        }

        public static void GetBlobs(string connectionString, string containerName)
        { 
            BlobServiceClient client = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            
            foreach (var blob in containerClient.GetBlobs())
            {
                Console.WriteLine($"Found: {blob.Name}");
            }
        }
    }
}
