using static System.Console;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace ManagedIdentity
{
    internal class Program
    { 
        private static string _tenantId = "";
        private static string _clientId = "";
        private static string _clientSecret = "";
        private static string _keyVaultUri = @"";
        private static string _secretName = "";
        
        static void Main(string[] args)

        {
            ClientSecretCredential clientCertificateCredential = new ClientSecretCredential(_tenantId, _clientId, _clientSecret);
            SecretClient secretClient = new SecretClient(new Uri(_keyVaultUri), clientCertificateCredential);
            var secret = secretClient.GetSecret(_secretName);
            WriteLine($"The secret value is {secret.Value.Value}");
        }
    }
}
