using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageUtils
{
    class Queue
    {
        public static void AddMessages(string queueName, string connectionString, List<string> messages)
        {
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            
            if (queueClient.Exists())
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(messages[i]);
                    var encodedMessage = Convert.ToBase64String(bytes);
                    queueClient.SendMessage(encodedMessage);
                    Console.WriteLine($"Message {messages[i]} has been sent.");
                }
            }
            Console.WriteLine($"All messages have been sent to {queueName}"); 
        }

        public static void PeekMessages(string queueName, string connectionString)
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
            foreach (var message in peekedMessages)
            {
                Console.WriteLine($"Found message: {message.MessageId}/{message.Body}");
            }
        }

        public static void ReceiveMessages(string queueName, string connectionString)
        {
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            if (queueClient.Exists())
            {
                QueueMessage[] messages = queueClient.ReceiveMessages(32);

                foreach (var message in messages)
                {
                    Console.WriteLine($"Received message {message.MessageId}/{message.Body}");
                }
            }
        }
    }
}
