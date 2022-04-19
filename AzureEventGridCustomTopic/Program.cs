using System;
using System.Collections.Generic;
using System.Text.Json;
using Azure;
using Azure.Messaging.EventGrid;

namespace AzureEventGridCustomTopic
{
    internal class Program
    {
        private static Uri _topic_endpoint = new Uri(@"https://az204-egtopic01.eastus-1.eventgrid.azure.net/api/events");
        private static AzureKeyCredential _credential = new AzureKeyCredential("enBVieMj0y5s7bdPikRGbqomEY3sY08Dhofdwf+pJuo=");

        static void Main(string[] args)
        {
            EventGridPublisherClient publisher = new EventGridPublisherClient(_topic_endpoint, _credential);

            Order order = new Order()
            {
                OrderId = "asdf3",
                Quantity = 3,
                UnitPrice = 9.99M
            };

            List<EventGridEvent> events = new List<EventGridEvent>()
            {
                new EventGridEvent("Placing new order", "app-neworder","1.0",JsonSerializer.Serialize(order))
            };
            
            publisher.SendEvents(events);
            Console.WriteLine("Events are sent.");
        }
    }
}
