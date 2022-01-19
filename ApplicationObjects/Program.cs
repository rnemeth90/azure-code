using System;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace ApplicationObjects
{
  internal class Program
  {
    private static string blobUrl = "";
    private static string localBlob = "";

    private static string tenantId = "";
    private static string clientId = "";
    private static string clientSecret = "";
    static void Main(string[] args)
    {
      ClientSecretCredential clientCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
      Uri blobUri = new Uri(blobUrl);
      BlobClient blobClient = new BlobClient(blobUri, clientCredential);
      blobClient.DownloadTo(localBlob);

      Console.WriteLine("Blob downloaded");
    }
  }
}
