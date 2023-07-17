using System.Runtime.Caching;

namespace DocumentCabinetLibrary
{
    public class DocumentCache : ICache<string, Document>
    {
        private MemoryCache _cache = MemoryCache.Default;

        public Document? Get(string key) => (Document)_cache.Get(key);

        public void Set(string key, Document value)
        {
            CacheItemPolicy policy;
            switch (value)
            {
                case Book _:
                    policy = new CacheItemPolicy();
                    break;
                case LocalizedBook _:
                    policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30)
                    };
                    break;
                case Magazine _:
                    policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(10)
                    };
                    break;
                default:
                    policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow
                    };
                    break;
            }
            _cache.Set(key, value, policy);
        }

        public void Remove(string key) => _cache.Remove(key);
    }
}
