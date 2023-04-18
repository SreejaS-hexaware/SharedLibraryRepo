using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Threading;

namespace RedisCacheLibrary
{
    public class RedisCacheService : IRedisCacheService
    {
        private ConnectionMultiplexer Redis = null;
        private IDatabase Database = null;
        private readonly object SyncLock = new();
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public RedisCacheService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public void EnsureConnection(string connectionString)
        {
            if (Database == null)
            {
                lock (SyncLock)
                {
                    if (Database == null)
                    {
                        //TODO: use appsettings 
                        Redis = ConnectionMultiplexer.Connect(connectionString);
                        Database = Redis.GetDatabase();
                    }
                }
            }
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var cachedData = await Database.StringGetAsync(key);
            return string.IsNullOrEmpty(cachedData) ?
              default : JsonSerializer.Deserialize<T>(cachedData, _jsonSerializerOptions);
        }
        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cache key cannot be null or empty.");
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Cache value cannot be null.");
            }
            var serializedValue = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            await Database.StringSetAsync(key, serializedValue, expiration);
        }
        public void RemoveAsync(string key)
        {
            Database.KeyDelete(key);
        }
    }
}