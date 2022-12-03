using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyService
{
    internal class GenericProxyCache<T>
    {
        private ObjectCache _cache = MemoryCache.Default;
        public T Get(string CacheItem)
        {

            T obj = (T)this._cache.Get(CacheItem);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                T res = default(T);
                if (res != null)
                {
                    this._cache.Add(CacheItem, res, null);
                }
                return res;
            }

        }
        public T Get(string CacheItem, double dt_seconds)
        {


            T obj = (T)this._cache.Get(CacheItem);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                T res = default(T);
                this._cache.Add(CacheItem, res, DateTimeOffset.Now.AddSeconds(dt_seconds));
                return res;

            }
        }
        public T Get(string CacheItem, DateTimeOffset dt)
        {
            T obj = (T)this._cache.Get(CacheItem);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                T res = default(T);
                this._cache.Add(CacheItem, res, dt);
                return res;

            }
        }
        public void Set(string CacheItem, T objectToStore)
        {
            this._cache.Add(CacheItem, objectToStore, null);

        }
        public void Set(string CacheItem, T objectToStore, double dt)
        {
            this._cache.Add(CacheItem, objectToStore, DateTimeOffset.Now.AddSeconds(dt));

        }
        public void Set(string CacheItem, T objectToStore, DateTimeOffset dt)
        {
            this._cache.Add(CacheItem, objectToStore, dt);

        }
    }
}
