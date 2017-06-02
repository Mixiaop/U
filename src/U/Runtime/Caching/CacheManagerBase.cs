using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using U.Dependency;
using U.Startup;

namespace U.Runtime.Caching
{
    /// <summary>
    /// Base class for cache manager
    /// It's used to simply implementing <see cref="ICacheManager"/>
    /// </summary>
    public abstract class CacheManagerBase : ICacheManager, ISingletonDependency
    {
        protected readonly ConcurrentDictionary<string, ICache> Caches;
        private readonly IUStartupConfiguration _configuration;
        protected CacheManagerBase(IUStartupConfiguration configuration)
        {
            _configuration = configuration;
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Caches.Values.ToImmutableList();
        }

        public virtual ICache GetCache(string name = "U")
        {
            return Caches.GetOrAdd(name, (cacheName) =>
            {
                var cache = CreateCacheImplementation(cacheName);

                var configurators = _configuration.Caching.Configurators.Where(c => c.CacheName == null || c.CacheName == cacheName);

                foreach (var configurator in configurators)
                {
                    if (configurator.InitAction != null)
                    {
                        configurator.InitAction(cache);
                    }
                }

                return cache;
            });
        }

        public virtual void Dispose()
        {
            foreach (var cache in Caches)
            {
                UPrimeEngine.Instance.IocManager.Release(cache.Value);
            }

            Caches.Clear();
        }

        /// <summary>
        /// Used to create actual cache implementation.
        /// </summary>
        /// <param name="name">Name of the cache</param>
        /// <returns>Cache object</returns>
        protected abstract ICache CreateCacheImplementation(string name);
    }
}
