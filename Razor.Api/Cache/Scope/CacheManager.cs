using Microsoft.Extensions.Caching.Memory;
using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.Web.Config;
using System;
using Microsoft.Extensions.Logging;

namespace Razor.Api.Web.Cache.Scope
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly CacheConfig _cacheConfig;
        private readonly ILogger<CacheManager> _logger;

        protected virtual string Prefix { get; set; } = "";

        public CacheManager(IMemoryCache cache, CacheConfig cacheConfig, ILogger<CacheManager> logger)
        {
            _cache       = cache;
            _cacheConfig = cacheConfig;
            _logger      = logger;
        }

        public bool TryGetValue<T>(string cacheKey, out T value)
        {
            var got = _cache.TryGetValue(cacheKey, out value);
            _logger.LogDebug("Cache.TryGetValue({cacheKey}): {got}", cacheKey, got);
            return got;
        }

        public void SetValue<T>(string cacheKey, T value)
        {
            _logger.LogDebug("Cache.SetValue({cacheKey}, {value})", cacheKey, value);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheConfig.AuthTokenExpirationS));
            _cache.Set(cacheKey, value, cacheEntryOptions);
        }

        public void RemoveValue(string cacheKey)
        {
            _logger.LogDebug("Cache.Remove({cacheKey})", cacheKey);
            _cache.Remove(cacheKey);
        }
    }
}
