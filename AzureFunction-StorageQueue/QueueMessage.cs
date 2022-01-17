using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunction_StorageQueue
{
    public class QueueMessage
    {
        [FunctionName("GetMessages")]
        public void Run([QueueTrigger("appqueue", Connection = "connectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
