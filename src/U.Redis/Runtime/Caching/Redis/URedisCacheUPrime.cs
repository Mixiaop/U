using System.Reflection;
using U.UPrimes;
using U.Runtime.Caching.Redis.Startup.Configuration;
using U.Runtime.Caching.Memory;

namespace U.Runtime.Caching.Redis
{
    [DependsOn(typeof(ULeadershipUPrime))]
    public class URedisCacheUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IRedisCacheConfiguration, RedisCacheConfiguration>();            
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            if (Engine.Resolve<IRedisCacheConfiguration>().Enabled == 1)
            {
                Engine.Register<ICacheManager, URedisCacheManager>();
            }
            else {
                Engine.Register<ICacheManager, UMemoryCacheManager>();
            }
        }
    }
}
