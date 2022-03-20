using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusQueueFunction
{
    public class ServiceBusQueueFunction
    {

        [FunctionName("ServiceBusQueueFunction")]
        public void Run([ServiceBusTrigger("orderqueue ", Connection = "connString")]string ServiceBusQueueFunction, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {ServiceBusQueueFunction}");
        }
    }
}
