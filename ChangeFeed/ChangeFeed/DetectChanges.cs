using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ChangeFeed
{
    public static class DetectChanges
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "FamilyDatabase",
            collectionName: "FamilyContainer",
            ConnectionStringSetting = "cosmosdbstring",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists =true)]IReadOnlyList<Family> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                foreach (var item in input)
                {
                    log.LogInformation($"Id: {item.Id}");
                    log.LogInformation($"Last Name: {item.LastName}");
                }
            }
        }
    }
}
