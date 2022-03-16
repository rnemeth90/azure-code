using System;
using System.Threading.Tasks;
using Azure.Data.Tables;

namespace TableStorage
{
    class Program
    {
        private static readonly string connectionString = "";
        private static readonly string tableName = "";
        static async Task<TableClient> Main(string[] args)
        {
            TableServiceClient client = new TableServiceClient(connectionString);
            TableClient tableClient = client.GetTableClient(tableName);
            var table = await tableClient.CreateIfNotExistsAsync();
        }
    }
}
