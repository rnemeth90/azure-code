using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ServiceBusApp
{
    class Program
    {
        private static string send_connection_string = "";
        private static string receive_connection_string = "";
        private static string topic_connection_string = "";
        private static string queue_name = "";
        private static string topic_name = "";
        private static string subscription_name = "";

        static void Main(string[] args)
        {
            SendMessages();
            ReceiveMessages();
        }
        public static void ReceiveMessages()
        {
            ServiceBusClient _client = new ServiceBusClient(topic_connection_string);
            ServiceBusReceiver _receiver = _client.CreateReceiver(topic_name, subscription_name, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete}) ;

            var _messages = _receiver.ReceiveMessagesAsync(20).GetAwaiter().GetResult();

            foreach (var _message in _messages)
            {
                Console.WriteLine(_message.SequenceNumber);
                Console.WriteLine(_message.Body.ToString());
                _receiver.CompleteMessageAsync(_message);
            }
            Console.ReadKey();
        }

        public static void SendMessages()
        {
            List<Order> _orders = new List<Order>()
            {
                new Order() {OrderID="O1",Quantity=10,UnitPrice=9.99m},
                new Order() {OrderID="O2",Quantity=15,UnitPrice=10.99m },
                new Order() {OrderID="O3",Quantity=20,UnitPrice=11.99m},
                new Order() {OrderID="O4",Quantity=25,UnitPrice=12.99m},
                new Order() {OrderID="O5",Quantity=30,UnitPrice=13.99m }
            };

            ServiceBusClient _client = new ServiceBusClient(topic_connection_string);
            ServiceBusSender _sender = _client.CreateSender(topic_name);

            foreach (Order _order in _orders)
            {
                ServiceBusMessage _message = new ServiceBusMessage(_order.ToString());
                _message.TimeToLive = TimeSpan.FromSeconds(60);
                _message.ContentType = "application/json";
                _message.ApplicationProperties.Add("department","devops");
                _message.ApplicationProperties.Add("env", "test");
                _sender.SendMessageAsync(_message).GetAwaiter().GetResult();
                Console.WriteLine($"Sending message: {_message.Body.ToString()}");
            }

            Console.WriteLine("All of the messages have been sent");
            Console.ReadKey();
        }
    }
}

