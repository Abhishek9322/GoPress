using GoPress.Application.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedDat = await _distributedCache.GetStringAsync(key);

            if(string.IsNullOrEmpty(cachedDat))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cachedDat);
        }

       
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var serializedData = JsonSerializer.Serialize(value);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(30)
            };

            await _distributedCache.SetStringAsync(key, serializedData, options);

        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key); 
        }

    }
}
