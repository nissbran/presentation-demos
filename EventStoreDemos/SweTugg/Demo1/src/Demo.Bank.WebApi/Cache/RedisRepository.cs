namespace Demo.Bank.WebApi.Cache
{
    using Newtonsoft.Json;
    using StackExchange.Redis;

    public class RedisRepository : IRedisRepository
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _redisDatabase;

        public RedisRepository()
        {
            _redisConnection = ConnectionMultiplexer.Connect("127.0.0.1,syncTimeout=5000");


            _redisDatabase = _redisConnection.GetDatabase();
        }

        public string Get(string key)
        {
            return _redisDatabase.StringGet(key);
        }

        public T Get<T>(string key) where T : new()
        {
            var value = _redisDatabase.StringGet(key);

            return value.IsNullOrEmpty ? new T() : JsonConvert.DeserializeObject<T>(value);
        }

        public void Set(string key, string value)
        {
            _redisDatabase.StringSet(key, value);
        }
        public void Set<T>(string key, T value)
        {
            _redisDatabase.StringSet(key, JsonConvert.SerializeObject(value));
        }
    }
}