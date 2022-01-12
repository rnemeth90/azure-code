using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageUtils
{
    class Queue
    {
        public static string AddMessages(string queueName, string connectionString, List<string> messages)
        {
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            
            if (queueClient.Exists())
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    queueClient.SendMessage(messages[i]);
                    Console.WriteLine($"Message {messages[i]} has been sent.");
                }
            }

            return $"All messages have been sent to {queueName}";
        }

        public static List<PeekedMessage> PeekMessages(string queueName, string connectionString)
        {
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            List<PeekedMessage> peekedMessages = new List<PeekedMessage>();

            if (queueClient.Exists())
            {
                PeekedMessage[] messages = queueClient.PeekMessages(32);
                foreach (PeekedMessage message in messages)
                {
                    peekedMessages.Add(message);
                }
            }

            return peekedMessages;
        }
    }
}
