using System;
using System.IO;
using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace ManagedIdentities
{
    internal class Program
    {
        private static string blobUrl = "";
        private static string localPath = "";
        static void Main(string[] args)
        {
            BlobClient blobClient = new BlobClient(new Uri(blobUrl), new DefaultAzureCredential());

            blobClient.DownloadTo(localPath);

            if (File.Exists(localPath))
            {
                Console.WriteLine("Download complete");
            }
            else
            {
                throw new FileNotFoundException();
            }

            Console.ReadKey();
        }
    }
}
