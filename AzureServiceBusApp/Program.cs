using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;


namespace AzureServibeBusQueueApp
{
    internal class Program
    {
        private const string _connectionString = @"Endpoint=sb://azrtnsb02.servicebus.windows.net/;SharedAccessKeyName=manage;SharedAccessKey=AHCsb/9498HZY1rMqwWgY4UxgqQmqxlm62Cy4IShMLY=;EntityPath=appqueue";
        private const string _topicConnectionString = @"Endpoint=sb://azrtnsb02.servicebus.windows.net/;SharedAccessKeyName=manage;SharedAccessKey=RQfwRD2OzSusfsdsquOp+PUhTSvVa8iuk+L44xi6Vro=;EntityPath=newtopic";
        
        private const string _queueName = "appqueue";
        static void Main(string[] args)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient(_connectionString);
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(_queueName);

            List<Order> orders = new List<Order>
            {
                new Order(){ OrderId="0001", Quantity=2, UnitPrice=9.99M }
            };

            foreach (var o in orders)
            {
                ServiceBusMessage message = new ServiceBusMessage(o.ToString());
                message.ContentType = "application/json";
                message.TimeToLive = TimeSpan.FromSeconds(30);
                serviceBusSender.SendMessageAsync(message).Wait();
            }

            PeekMessagesAsync(_queueName, 2);
        }



        public static IReadOnlyList<ServiceBusReceivedMessage> PeekMessagesAsync(string queueName, int messageCount)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient(_connectionString);
            ServiceBusReceiver receiver = serviceBusClient
                .CreateReceiver(queueName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });
            return receiver.ReceiveMessagesAsync(messageCount).GetAwaiter().GetResult();
        }
    }
}
