using System;
using StackExchange.Redis;

namespace RedisApp01
{
    internal class Program
    {
        private const string connectionString = "azrtnrediscache.redis.cache.windows.net:6380,password=v1QEw1jHdI2h41mz1MaA0j1XT4tTG4J56AzCaKETvU4=,ssl=True,abortConnect=False";
        static void Main(string[] args)
        {
            
            IDatabase db = ConnectionMultiplexer.Connect(connectionString).GetDatabase();

            Console.WriteLine($"[ReadCache] {db.StringGet("Session333").ToString()}");
            Console.WriteLine($"[WriteCache] {db.StringSet("Session333", "Writing to Redis @" + DateTime.Now)}");
            Console.WriteLine($"[ReadCache] {db.StringGet("Session333").ToString()}");
            db.KeyExpire("Session333", DateTime.Now.AddMinutes(1));
        }
    }
}
