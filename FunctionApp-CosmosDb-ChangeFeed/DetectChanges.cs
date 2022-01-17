using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp_CosmosDb_ChangeFeed
{
    public static class DetectChanges
    {
        [FunctionName("ReadChange")]
        public static void Run([CosmosDBTrigger(
            databaseName: "appdb",
            collectionName: "course",
            ConnectionStringSetting = "connectionString",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists =true)]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                foreach (var course in input)
                {
                    log.LogInformation($"Course id: {course.Id}");
                    log.LogInformation($"Course id: {course.GetPropertyValue<string>("courseid")}");
                    log.LogInformation($"Course name: {course.GetPropertyValue <string>("coursename")}");
                    log.LogInformation($"Course Rating: {course.GetPropertyValue<decimal>("rating")}");
                }
            }
        }
    }
}
