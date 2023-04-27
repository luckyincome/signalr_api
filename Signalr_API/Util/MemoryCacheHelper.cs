using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Signalr_API.Util
{
    using System;
    using System.Runtime.Caching;

    public static class MemoryCacheHelper
    {
        private static MemoryCache cache = MemoryCache.Default;


        public static bool Exists(String key)
        {
            if (key == null)
            {
                return false;
            }
            return cache.Get(key)==null?false:true;
        }
        public static T Get<T>(string key) where T : class
        {
            if (key == null)
            {
                return null;
            }
            return cache.Get(key) as T;
        }

        public static bool Add<T>(string key, T value, DateTimeOffset expiration) where T : class
        {
            return cache.Add(key, value, expiration);
            
        }

        public static void Remove(string key)
        {
            cache.Remove(key);
        }

        public static void Clear()
        {
            foreach (var item in cache)
            {
                cache.Remove(item.Key);
            }
        }
    }

}
