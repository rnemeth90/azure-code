using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace AzFunctionHttpTrigger
{
    public static class GetEvents
    {
        [FunctionName("ReadEvents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            StreamReader stream = new(req.Body);
            log.LogInformation($"[RequestBody] {stream.ReadToEnd()}\n");

            EventGridSubscriber subscriber = new();

            foreach (var e in subscriber.DeserializeEventGridEvents(stream.ReadToEnd()))
            {
                if (e.Data is SubscriptionValidationEventData)
                { 
                    SubscriptionValidationEventData data = (SubscriptionValidationEventData)e.Data;
                    log.LogInformation($"[ValidationCode] {data.ValidationCode}\n");
                    log.LogInformation($"[ValidationUrl] {data.ValidationUrl}\n");
                    SubscriptionValidationResponse response = new()
                    {
                        ValidationResponse = data.ValidationCode
                    };
                    return new OkObjectResult(response);
                }
            }
            return new OkObjectResult(string.Empty);
        }
    }
}
