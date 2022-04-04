
using Microsoft.Azure.Cosmos;
using CosmosDb.Shared;


namespace CosmosDb.Shared;
public class Program
{ 
    internal static string _connectionString = "";
    internal static string _databaseName = "";
    internal static string _containerName = "";

    static DatabaseResponse ConnectToCosmosAndCreateDatabase()
    { 
        CosmosClient client = new CosmosClient(_connectionString);
        return client.CreateDatabaseIfNotExistsAsync(_databaseName).GetAwaiter().GetResult();
    }


}


Program