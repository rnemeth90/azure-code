using static System.Console;
using Azure.Storage.Blobs;
using Azure.Identity;
using System;

namespace AppObject_DownloadFromBlob
{
    internal class Program
    {

        private static readonly string _fileName = @"C:\users\Ryan.Nemeth\Downloads\test.jpg";
        private static readonly string _blobName = "deathmagnetic.jpg";

        private static string _tenantId = "";
        private static string _clientId = "";
        private static string _clientSecret = "";

        static void Main(string[] args)
        {
            ClientSecretCredential clientCred = new ClientSecretCredential(_tenantId, _clientId, _clientSecret);
            Uri uri = new Uri(@"https://azrtnmusicapistorage.blob.core.windows.net/album-art/deathmagnetic.png");

            BlobClient blobClient = new BlobClient(uri, clientCred);
            blobClient.DownloadTo(_fileName);
            WriteLine("Blob downloaded");
        }
    }
}
