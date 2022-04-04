using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisApp
{
    internal class RedisClass
    {
        private Lazy<ConnectionMultiplexer> _connection;

        public ConnectionMultiplexer Connection { get => _connection.Value; }

        public RedisClass()
        {
            _connection = CreateConnection();
        }

        public static Lazy<ConnectionMultiplexer> CreateConnection()
        {
            string connectionString = "azrtnrediscache.redis.cache.windows.net:6380,password=v1QEw1jHdI2h41mz1MaA0j1XT4tTG4J56AzCaKETvU4=,ssl=True,abortConnect=False";

            return new Lazy<ConnectionMultiplexer>(() =>
            { 
                return ConnectionMultiplexer.Connect(connectionString);
            });

        }

        private void SetValues(Dictionary<RedisKey,RedisValue> dictionary)
        {
            IDatabase db = Connection.GetDatabase();
            db.StringSet(dictionary);
        }
    }
}
