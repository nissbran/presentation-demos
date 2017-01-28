namespace Demo.Bank.WebApi.Cache
{
    public interface IRedisRepository
    {
        string Get(string key);
        T Get<T>(string key) where T : new();
        void Set(string key, string value);
        void Set<T>(string key, T value);
    }
}