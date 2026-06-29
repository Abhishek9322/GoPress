using GoPress.Application.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
             _memoryCache = memoryCache;
        }
        public Task<T?> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        
        public Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
           var options=new MemoryCacheEntryOptions
           {
               AbsoluteExpirationRelativeToNow = expiration
           };
            _memoryCache.Set(key, value, options);
            return Task.CompletedTask;
        }
        public Task RemoveAsync(string key)
        {
          _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

    }
}
