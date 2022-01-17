using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageUtils
{
    internal class Table
    {
        // Create a table
        public static void Create(string tableName, string connectionString)
        {
            TableServiceClient tableClient = new TableServiceClient(connectionString);
            TableItem table = tableClient.CreateTableIfNotExists(tableName);
            Console.WriteLine($"Table {tableName} created!");
        }

        // Delete a table
        public static void Delete(string tableName, string connectionString)
        {
            TableServiceClient tableClient = new TableServiceClient(connectionString);
            tableClient.DeleteTable(tableName);
            Console.WriteLine($"Table {tableName} deleted!");
        }

        // List rows in a table
        public static List<TableItem> List(string tableName, string connectionString)
        {
            TableServiceClient tableClient = new TableServiceClient(connectionString);
            Pageable<TableItem> tables = tableClient.Query(filter: $"TableName eq '{tableName}'");
            List<TableItem> tableList = new List<TableItem>();

            foreach (TableItem table in tables)
            {
                tableList.Add(table);
            }
            return tableList;
        }

        public static void InsertEntity(Azure.Data.Tables.TableEntity entity, string tableName, string connectionString)
        {
            TableServiceClient client = new TableServiceClient(connectionString);
            TableClient table = client.GetTableClient(tableName);
            table.AddEntity(entity);
            Console.WriteLine("Entity added.");
        }

        public static void DeleteEntity(Azure.Data.Tables.TableEntity entity, string tableName, string connectionString)
        {
            TableServiceClient client = new TableServiceClient(connectionString);
            TableClient table = client.GetTableClient(tableName);
            table.DeleteEntity(entity.PartitionKey, entity.RowKey);
            Console.WriteLine($"Entity Deleted.");
        }
    }
}
