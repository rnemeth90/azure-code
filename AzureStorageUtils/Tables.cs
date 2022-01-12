using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageUtils
{
    class Tables
    {
        public static string connectionString = "";
        public static string tableName = "";

        public static string Create(string tableName, string connectionString)
        {
            TableServiceClient tableClient = new TableServiceClient(connectionString);
            TableItem table = tableClient.CreateTableIfNotExists(tableName);
            return $"{tableName} created!";
        }

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

        public static string Delete(string tableName, string connectionString)
        {
            TableServiceClient tableClient = new TableServiceClient(connectionString);
            tableClient.DeleteTable(tableName);
            return $"{tableName} deleted!";
        }


    }
}
