using System;
using System.Collections.Generic;
using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubs
{
    internal class Program
    {
        private static string connString = "";
        static void Main(string[] args)
        {
            List<Order> _orders = new List<Order>
            {
                new Order { OrderId="01",Quantity=10,UnitPrice=9.99m,DiscountCategory="Tier 1"},
                new Order { OrderId="02",Quantity=15,UnitPrice=10.99m,DiscountCategory="Tier 2"},
                new Order { OrderId="03",Quantity=20,UnitPrice=11.99m,DiscountCategory="Tier 3"},
                new Order { OrderId="04",Quantity=25,UnitPrice=12.99m,DiscountCategory="Tier 1"},
                new Order { OrderId="05",Quantity=30,UnitPrice=13.99m,DiscountCategory="Tier 1"}
            };

            EventHubProducerClient _producer_client = new EventHubProducerClient(connString);
            EventDataBatch _batch = _producer_client.CreateBatchAsync().GetAwaiter().GetResult();
            foreach (var order in _orders)
            {
                Console.WriteLine($"Adding {order.ToString()} to the event hub");
                _batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(order.ToString())));
            }
            _producer_client.SendAsync(_batch).GetAwaiter().GetResult();
            Console.WriteLine("Batch sent.");
            Console.ReadKey();
        }
    }
}

