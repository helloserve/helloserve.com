using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model.Cache
{
    public static class ModelCache
    {
        private class CacheItem
        {
            public DateTime ExpireTime;
            public object Value;
        }

        private static object _cacheLock = new object();

        private static ConcurrentDictionary<string, CacheItem> _cache;

        static ModelCache()
        {
            lock (_cacheLock)
            {
                _cache = new ConcurrentDictionary<string, CacheItem>();
            }
        }

        public static void Add(string key, object value, int timeout = 30 * 60 * 1000)
        {
            lock (_cacheLock)
            {
                CacheItem item = new CacheItem()
                {
                    ExpireTime = DateTime.Now.AddMilliseconds(timeout),
                    Value = value
                };
                _cache.AddOrUpdate(key, item, (k, v) => { return item; });
            }
        }

        public static object Get(string key)
        {
            lock (_cacheLock)
            {
                CacheItem item;
                if (_cache.TryGetValue(key, out item))
                {
                    if (item.ExpireTime < DateTime.Now)
                        return null;

                    return item.Value;
                }
                return null;
            }
        }
    }
}
