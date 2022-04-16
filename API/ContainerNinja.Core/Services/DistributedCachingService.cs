using ContainerNinja.Contracts.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ContainerNinja.Core.Services
{
    public class DistributedCachingService : ICachingService
    {
        private readonly IDistributedCache _cache;

        private readonly DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromMinutes(60)
        };

        public DistributedCachingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T? GetItem<T>(string cacheKey)
        {
            var item = _cache.GetString(cacheKey);
            if (item != null)
            {
                return JsonConvert.DeserializeObject<T>(item);
            }
            return default;
        }

        public T SetItem<T>(string cacheKey, T item)
        {
            _cache.SetString(cacheKey, JsonConvert.SerializeObject(item), options);
            return item;
        }
    }
}
