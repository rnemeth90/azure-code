using StackExchange.Redis;
using System;

namespace AzureRedisApp
{
    internal class Program
    {
        private static Lazy<ConnectionMultiplexer> _connectionMultiplexer = CreateConnection();

        public static ConnectionMultiplexer Connection { get => _connectionMultiplexer.Value; }

        static void Main(string[] args)
        {
            var db = Connection.GetDatabase();
            db.StringSet("CourseName", "History");
            db.StringSet("CourseName", "Geography");

            Console.WriteLine(db.StringGet("CourseName"));

            Console.ReadKey();
        }

        private static Lazy<ConnectionMultiplexer> CreateConnection()
        {
            string cache_connectionString = "azrtnrediscache.redis.cache.windows.net:6380,password=v1QEw1jHdI2h41mz1MaA0j1XT4tTG4J56AzCaKETvU4=,ssl=True,abortConnect=False";
            return new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(cache_connectionString);
            });
        }
    }
}
