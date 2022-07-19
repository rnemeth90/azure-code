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
        private static string connectionString = "";
        private const string blobStorageConnectionString = "";
        private const string blobContainerName = "";
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
