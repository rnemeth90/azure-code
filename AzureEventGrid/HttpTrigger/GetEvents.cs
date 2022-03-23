using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using Azure.Messaging.EventGrid;

namespace HttpTrigger
{
    public static class GetEvents
    {
        [FunctionName("GetEvents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            StreamReader reader = new StreamReader(req.Body);
            log.LogInformation(reader.ReadToEnd());

            EventGridSubscriber sub = new EventGridSubscriber();
            EventGridEvent[] events = sub.DeserializeEventGridEvents(req.Body);
        }
    }
}
