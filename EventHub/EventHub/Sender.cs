using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHub
{
    internal class Program
    {
        private static string connectionString = "Endpoint=sb://azrtneventhub01.servicebus.windows.net/;SharedAccessKeyName=connstring;SharedAccessKey=v233BlYdz44dLp8OEw3wC7A0QQDNlQ4K+V3MR6K2l5Q=;EntityPath=apphub";
        static List<Order> orders = new List<Order>();

        static async Task Main()
        {
            while (true)
            {
                EventHubProducerClient producerClient = new EventHubProducerClient(connectionString);
                using (EventDataBatch eventBatch = await producerClient.CreateBatchAsync())
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        orders.Add(new Order { OrderId = i, DiscountCategory="none", Quantity=1, UnitPrice=9.99m });

                        if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(orders.ToString()))))
                        {
                            // if it is too large for the batch
                            throw new Exception($"Order {orders.Find(p => p.OrderId == i)} is too large for the batch and cannot be sent.");
                        }
                    }
                    try
                    {
                        // Use the producer client to send the batch of events to the event hub
                        await producerClient.SendAsync(eventBatch);
                        Console.WriteLine($"A batch of {5} events has been published.");
                    }
                    finally
                    {
                        await producerClient.DisposeAsync();
                    }
                }
            }
        }
    }
}
