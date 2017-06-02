using U;
using U.Dependency;
using U.Startup.Configuration;
using U.Runtime.Caching.Redis.Startup.Configuration;
using U.Runtime.Caching.Memory;

namespace U.Runtime.Caching.Redis
{
    public static class URedisCacheUPrimeConfigurationExtensions
    {
        public static void UseRedis(this ICachingConfiguration cachingConfiguration)
        {
            if (UPrimeEngine.Instance.Resolve<IRedisCacheConfiguration>().Enabled == 1)
            {
                cachingConfiguration.UConfiguration.Engine.IocManager.RegisterIfNot<ICacheManager, URedisCacheManager>();
            }
            else {
                cachingConfiguration.UConfiguration.Engine.IocManager.RegisterIfNot<ICacheManager, UMemoryCacheManager>();
            }
        }
    }
}
