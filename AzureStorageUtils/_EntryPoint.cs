using System;
using System.Collections;
using System.Collections.Generic;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Queues.Models;
using AzureStorageUtils;

namespace AzureStorageUtils
{
  class _EntryPoint
  {
    private static string connection_string = "";
    private static string container_name = "";
    private static string table_name = "";
    private static string queueName = "";

    static void Main(string[] args)
    {
            //Table.Create(table_name, connection_string);
            //List<TableEntity> products = new List<TableEntity>
            //{
            //    new TableEntity("ProductA", "Widget"),
            //    new TableEntity("ProductB", "Whoza"),
            //    new TableEntity("ProductC", "Wingding")
            //};

            //foreach (var item in products)
            //{
            //    Table.InsertEntity(item, table_name, connection_string);
            //}
            int messageCount = 20;
            List<string> list = new List<string>();
            for (int i = 0; i < messageCount; i++)
            {
                list.Add($"message{i}");
            }
            Queue.AddMessages(queueName, connection_string, list);
            //Queue.PeekMessages(queueName,connection_string);
            //Console.WriteLine("///////////////////////////////");
            //Queue.ReceiveMessages(queueName,connection_string); 
            //Console.WriteLine($"{blobProperties.AccessTier}");
        }
  }
}
