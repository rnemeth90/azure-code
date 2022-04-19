using Azure;
using Azure.Messaging.EventGrid;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace EventGridCustomTopic
{
    internal class Program
    {
        public static Uri _topicEndpoint;
        public static AzureKeyCredential _topicAccessKey;
        static void Main(string[] args)
        {
            _topicEndpoint = new Uri("https://apptopic.eastus-1.eventgrid.azure.net/api/events");
            _topicAccessKey = new AzureKeyCredential("idebgCm8JhyNne8VqpO0Rn5DPJkWlTVb+0Grv9twgLU=");

            EventGridPublisherClient client = new EventGridPublisherClient(_topicEndpoint, _topicAccessKey);

            var order = new Order
            {
                OrderId = "123",
                Quantity = 1,
                Price = 9.99M
            };

            List<EventGridEvent> events = new List<EventGridEvent>()
            { 
                new EventGridEvent(order.OrderId,"app.OrderId","1",JsonSerializer.Serialize(order))
            };

            client.SendEvents(events);
            Console.WriteLine("Event has sent");
        }
    }
}
