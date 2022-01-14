using System;
using System.Collections;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Queues.Models;
using AzureStorageUtils;

namespace AzureStorageUtils
{
  class _EntryPoint
  {
    private static string connection_string = "";
    private static string container_name = "";
    private static string blob_name = "";

    static void Main(string[] args)
    {
      BlobServiceClient blobServiceClient = new BlobServiceClient(connection_string);
      BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container_name);
      BlobClient blobClient = containerClient.GetBlobClient(blob_name);

      BlobProperties blobProperties = blobClient.GetProperties();

      IDictionary<string, string> _metadata = blobProperties.Metadata;


      foreach (var item in _metadata)
      {
        Console.WriteLine($"{item.Key}:{item.Value}");
      }

      //Console.WriteLine($"{blobProperties.AccessTier}");
    }
  }
}
