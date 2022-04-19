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

namespace EventGridHttpTrigger
{
    public static class GetEvent
    {
        [FunctionName("ReadEvents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            StreamReader reader = new StreamReader(req.Body);
            string body = reader.ReadToEnd();   
            Console.WriteLine($"[Body]: {body}");
            
            EventGridSubscriber sub = new EventGridSubscriber();
            EventGridEvent[] events = sub.DeserializeEventGridEvents(body);

            foreach (var e in events)
            {
                if (e.Data is SubscriptionValidationEventData)
                {
                    SubscriptionValidationEventData data = (SubscriptionValidationEventData)e.Data;
                    log.LogInformation($"[Validation Code]: {data.ValidationCode}");
                    log.LogInformation($"[Validation URL]: {data.ValidationUrl}");

                    SubscriptionValidationResponse response = new SubscriptionValidationResponse
                    {
                        ValidationResponse = data.ValidationCode
                    };
                    return new OkObjectResult(response);
;               }
                else
                {
                    log.LogInformation($"[Data]: {e.Data.ToString()}");
                }
            }
            return new OkResult();   
        }
    }
}
