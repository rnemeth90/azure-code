using static System.Console;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace ManagedIdentity
{
    internal class Program
    { 
        private static string _tenantId = "e9fc6d10-80ac-4eb7-8d23-f2a02ca63d76";
        private static string _clientId = "206dd9c1-3247-4f9c-a417-cc73dd0051ab";
        private static string _clientSecret = "k2i7Q~Zu_z2~JJUUnoWW.kd_f~2NXwHxKX.-K";
        private static string _keyVaultUri = @"https://test-kv-us-e-rtn-01.vault.azure.net/";
        private static string _secretName = "mysecret";
        
        static void Main(string[] args)

        {
            ClientSecretCredential clientCertificateCredential = new ClientSecretCredential(_tenantId, _clientId, _clientSecret);
            SecretClient secretClient = new SecretClient(new Uri(_keyVaultUri), clientCertificateCredential);
            var secret = secretClient.GetSecret(_secretName);
            WriteLine($"The secret value is {secret.Value.Value}");
        }
    }
}
