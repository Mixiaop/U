using System;

namespace U.Runtime.Caching
{
    public class NullCache : CacheBase
    {
        public NullCache(string name)
            : base(name)
        {
        }

        public override object GetOrDefault(string key)
        {
            return null;
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            
        }

        public override void Remove(string key)
        {
            
        }

        public override void RemoveByPattern(string pattern)
        {
            
        }

        public override void Clear()
        {
        }

        public override void Dispose()
        {
        }
    }
}
