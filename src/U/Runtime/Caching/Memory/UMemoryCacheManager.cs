using System.Runtime.Caching;
using U.Dependency;
using U.Startup;

namespace U.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements<see cref="ICacheManager"/> to work with <see cref="MemoryCache"/>
    /// </summary>
    public class UMemoryCacheManager : CacheManagerBase
    {
        public UMemoryCacheManager(IUStartupConfiguration configuration)
            : base(configuration)
        {
            //UPrime.Instance.IocManager.RegisterIfNot<UMemoryCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            //return UPrime.Instance.Resolve<UMemoryCache>(new { name });

            return new UMemoryCache(name);
        }
    }
}
