// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace AzureEventGrid
{
    public static class BlobFunction
    {
        [FunctionName("GetBlobDetails")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.EventTime.ToString());
            log.LogInformation(eventGridEvent.EventType.ToString());    
            log.LogInformation(eventGridEvent.Subject.ToString());
            log.LogInformation(eventGridEvent.Topic.ToString());
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
