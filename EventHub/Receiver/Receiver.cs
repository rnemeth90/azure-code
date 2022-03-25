using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    internal class Receiver
    {
        private static string connectionString = "Endpoint=sb://azrtneventhub01.servicebus.windows.net/;SharedAccessKeyName=connstring;SharedAccessKey=v233BlYdz44dLp8OEw3wC7A0QQDNlQ4K+V3MR6K2l5Q=;EntityPath=apphub";
        private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountrneme8f24;AccountKey=jAhzgzRLljSrgcqDFcRTARibzpjw/yCyaVgep57GErz2UvWvYel0vfpvzARWhXOZwDx/eyCP/ANJRdTbOeCwug==;EndpointSuffix=core.windows.net";
        private const string blobContainerName = "eventhub";
        static BlobContainerClient storageClient;
        static EventProcessorClient processor;

        static async Task Main(string[] args)
        {
            while (true)
            {
                // Read from the default consumer group: $Default
                string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

                // Create a blob container client that the event processor will use 
                storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

                // Create an event processor client to process events in the event hub
                processor = new EventProcessorClient(storageClient, consumerGroup, connectionString);

                // Register handlers for processing events and handling errors
                processor.ProcessEventAsync += ProcessEventHandler;
                processor.ProcessErrorAsync += ProcessErrorHandler;

                // Start the processing
                await processor.StartProcessingAsync();

                // Wait for 30 seconds for the events to be processed
                await Task.Delay(TimeSpan.FromSeconds(30));

                // Stop the processing
                await processor.StopProcessingAsync();
            }
        }

        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            Console.WriteLine($"\t[SequenceNumber] { eventArgs.Data.SequenceNumber}");
            Console.WriteLine(Encoding.UTF8.GetString(eventArgs.Data.EventBody));
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"[ERROR] {eventArgs.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
