using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AzureServiceBusQueue
{
    internal class Program
    {
        private static string _connectionString = @"Endpoint=sb://azrtnsb02.servicebus.windows.net/;SharedAccessKeyName=manage;SharedAccessKey=GtAWU5UBXO2aQsyF2B0RFVNNmyHuCttkiKWVI3WoUL4=;EntityPath=apptopic01";
        private static string _queueName = "duplicateDetectionQueue";
        private static string _topicName = "apptopic01";
        private static string _subscriptionName = "SubscriptionA";

        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { OrderId=1, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=2, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=3, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=4, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=5, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=6, Quantity=1, UnitPrice= 9.99M}
            };

            ServiceBusClient client = new ServiceBusClient(_connectionString);
            ServiceBusSender sender = client.CreateSender(_topicName);

            foreach (var o in orders)
            {
                var message = new ServiceBusMessage(o.ToString());
                message.ContentType = "application/json";
                message.TimeToLive = TimeSpan.FromSeconds(500);
                message.MessageId = o.OrderId.ToString();
                message.ApplicationProperties.Add("CreatedBy","John");
                message.ApplicationProperties.Add("Department", "Finance");
                sender.SendMessageAsync(message).GetAwaiter().GetResult();
                Console.WriteLine($"Order with Id: {o.OrderId} sent successfully.");
            }

            //PeekMessageAsync();

        }

        static void PeekMessageAsync()
        {
            ServiceBusClient client = new ServiceBusClient(_connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(_topicName, _subscriptionName, new ServiceBusReceiverOptions() { ReceiveMode=ServiceBusReceiveMode.PeekLock });
            var message = receiver.ReceiveMessageAsync().GetAwaiter().GetResult();
            Console.WriteLine($"[TTl {message.TimeToLive}]{message.Body.ToString()}");
        }

        static void ReceiveMessagesAsync()
        {
            ServiceBusClient client = new ServiceBusClient(_connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(_queueName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

            foreach (var m in receiver.ReceiveMessagesAsync(10).GetAwaiter().GetResult())
            {
                Console.WriteLine(m.Body.ToString());
            }
        }
    }
}
