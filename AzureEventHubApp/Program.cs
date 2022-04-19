using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureEventHubApp
{
    internal class Program
    {
        private static string _connectionString = @"Endpoint=sb://azrtneventhub01.servicebus.windows.net/;SharedAccessKeyName=manage;SharedAccessKey=eFDTkfs+wd73mcfR2B1GiDOgAvrseDtbDLyqEoku0K4=;EntityPath=multipartitionhub";
        private static string _consumerGroup = "$Default";
        private static int _timeout = 5000;
        static void Main(string[] args)
        {
            var orderNumber = 0;
            var count = 0;
            List<Order> orders = new List<Order>();
            List<string> products = new List<string>
            {
                "Bananas",
                "Apples",
                "Oranges",
                "Mangos",
                "Pears",
                "Grapes"
            };

            while (count < 50)
            {
                var messageId = Guid.NewGuid().ToString();
                var quantity = new Random().Next(1,20);
                var price = new Random().NextDouble();
                orders.Add(
                    new Order
                    {
                        MessageId = messageId,
                        OrderId = orderNumber,
                        Name = products[(new Random().Next(0, products.Count))],
                        Quantity = quantity,
                        UnitPrice = Math.Round(price, 2)
                    }); 
                orderNumber++;
                count++;
            }

            SendOrdershToEventHubAsync(orders, _connectionString);
            Thread.Sleep(_timeout);
            ReceiveOrdersFromEventHubAsync(_consumerGroup, _connectionString);

            Console.ReadLine();
        }

        static async void SendOrdershToEventHubAsync(List<Order> orders, string connectionString)
        { 
            EventHubProducerClient client = new EventHubProducerClient(connectionString);
            EventDataBatch batch = client.CreateBatchAsync().GetAwaiter().GetResult();

            foreach (var i in orders)
            {
                batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(i.ToString())));
                Console.WriteLine($"[Batch]: {i.ToString()}");
            }
            Console.WriteLine("Sending batch");
            await client.SendAsync(batch);
        }

        static async void ReceiveOrdersFromEventHubAsync(string consumerGroup, string connectionString)
        {
            EventHubConsumerClient client = new EventHubConsumerClient(consumerGroup, connectionString);
            string[] partitionsIds =  await client.GetPartitionIdsAsync();

            for (int i = 0; i < partitionsIds.Length; i++)
            {
                await foreach (var e in client.ReadEventsFromPartitionAsync(partitionsIds[i], EventPosition.Earliest ))
                {
                    Console.WriteLine($"[PartitionId]: {e.Partition.PartitionId}");
                    Console.WriteLine($"[SequenceNumber]: {e.Data.SequenceNumber}");
                    Console.WriteLine($"[MessageId]: {e.Data.MessageId}");
                    Console.WriteLine($"[Offset]: {e.Data.Offset}");
                    Console.WriteLine($"[EventBody]: {e.Data.EventBody}");
                    Console.WriteLine();
                }
            }
        }
    }
}
