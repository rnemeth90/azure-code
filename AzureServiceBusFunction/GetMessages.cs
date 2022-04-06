using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusFunction
{
    public static class GetMessages
    {
        [FunctionName("GetMessages")]
        public static void Run([ServiceBusTrigger("appqueue", Connection = "queue-connectionstring")]Message myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
