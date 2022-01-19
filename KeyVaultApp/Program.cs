using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace KeyVaultApp
{
    internal class Program
    {
        // This program uses an application object in Azure AD to retrieve
        // the value of a secret stored in a Key Vault

        private static string _tenantId = "";
        private static string _clientId = "";
        private static string _clientSecret = "";
        private static string _keyVaultUri = "";
        private static string _secretName = "";

        static void Main(string[] args)
        {
            ClientSecretCredential clientCredential = new ClientSecretCredential(_tenantId, _clientId, _clientSecret);
            SecretClient secretClient = new SecretClient(new Uri(_keyVaultUri), clientCredential);
            var secret = secretClient.GetSecret(_secretName);
            Console.WriteLine($"Found secret: value={secret.Value}");
            Console.ReadKey();

        }
    }
}
