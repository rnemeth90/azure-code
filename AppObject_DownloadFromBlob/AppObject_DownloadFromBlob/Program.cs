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

        private static string _tenantId = "e9fc6d10-80ac-4eb7-8d23-f2a02ca63d76";
        private static string _clientId = "206dd9c1-3247-4f9c-a417-cc73dd0051ab";
        private static string _clientSecret = "k2i7Q~Zu_z2~JJUUnoWW.kd_f~2NXwHxKX.-K";

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
