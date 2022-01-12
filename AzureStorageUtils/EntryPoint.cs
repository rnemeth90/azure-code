using System;
using System.Collections;
using System.Collections.Generic;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Queues.Models;
using AzureStorageUtils;

namespace AzureStorageUtils
{
    class EntryPoint
    {
        private static string connection_string = "";
        private static string container_name = "";
        private static string queue_name = "";

        static void Main(string[] args)
        {

            var messages = Queue.PeekMessages(queue_name, connection_string);
            foreach (var message in messages)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Message Body: {message.Body.ToString()}\n" +
                    $"Message Id: {message.MessageId}\n");
            }

        }
    }
}
