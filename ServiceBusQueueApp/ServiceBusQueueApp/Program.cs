using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;

namespace ServiceBusQueueApp
{
    internal class Program
    {
        private static string queueConnectionString = "Endpoint=sb://azrtnsb02.servicebus.windows.net/;SharedAccessKeyName=key1;SharedAccessKey=qnAr1poGCjce5tsXxjQ4mCKkZmHFiX+h594Yzk67pfw=;EntityPath=orderqueue";
        private static string topicConnectionString = "Endpoint=sb://azrtnsb02.servicebus.windows.net/;SharedAccessKeyName=manage;SharedAccessKey=RQfwRD2OzSusfsdsquOp+PUhTSvVa8iuk+L44xi6Vro=;EntityPath=newtopic";
        private static string queueName = "orderqueue";
        private static string topicName = "newtopic";
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>
            {
                new Order { OrderId="01",Quantity=10,UnitPrice=9.99m },
                new Order { OrderId="02",Quantity=15,UnitPrice=5.99m },
                new Order { OrderId="03",Quantity=13,UnitPrice=2.99m },
                new Order { OrderId="04",Quantity=14,UnitPrice=1.99m },
                new Order { OrderId="05",Quantity=15,UnitPrice=6.99m },
                new Order { OrderId="06",Quantity=112,UnitPrice=3.99m },
            };

            SendOrders(orders);
            //PeekOrders();
        }

        public static void PeekOrders()
        {
            ServiceBusClient client = new ServiceBusClient(queueConnectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(queueName, new ServiceBusReceiverOptions() { 
                                                                                ReceiveMode=ServiceBusReceiveMode.PeekLock });
            var messages = receiver.ReceiveMessagesAsync(3).GetAwaiter().GetResult();

            foreach (var m in messages)
            {
                Console.WriteLine($"Receiving message: [{m.Body.ToString()}], DeadLetter: {m.DeadLetterErrorDescription}");
            }
        }

        public static void SendOrders(IList<Order> items)
        {

            ServiceBusClient client = new ServiceBusClient(queueConnectionString);
            ServiceBusSender sender = client.CreateSender(queueName);

            int c = 0;
            foreach (var i in items)
            {
                ServiceBusMessage message = new ServiceBusMessage(i.ToString());
                message.ContentType = "application/json";
                message.TimeToLive = TimeSpan.FromSeconds(30);
                message.MessageId = c.ToString();
                c++;
                sender.SendMessageAsync(message).Wait();
                Console.WriteLine($"Sending message: [{i.ToString()}]");
            }
        }
    }
}
