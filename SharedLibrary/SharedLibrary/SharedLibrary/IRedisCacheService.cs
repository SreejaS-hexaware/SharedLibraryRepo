using System.Threading.Tasks;
using System;

namespace RedisCacheLibrary
{
    public interface IRedisCacheService
    {
        public void EnsureConnection(string connectionString);
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        public void RemoveAsync(string key);
    }
}