using System.Reflection;
using System.Net;
using U.UPrimes;
using U.Hangfire;
using U.Runtime.Caching.Redis;
using U.Dapper;
using U.EntityFramework;
using U.Utilities.Web;
using U.Runtime.Caching;
using U.Tests.Web.Caching;

namespace U.Tests.Web
{
    [DependsOn(
        //typeof(UFrameworkUPrime),
        typeof(ULeadershipUPrime),
        typeof(UHangfireUPrime),
        typeof(URedisCacheUPrime),
        typeof(UDapperUPrime),
        typeof(UEntityFrameworkUPrime)
        )]
    public class UTestsWebUPrime : U.UPrimes.UPrime
    {
        public override void PreInitialize()
        {
            Engine.Configuration.BackgroundJob.IsJobExecutionEnabled = false;
            Engine.Configuration.Settings.IsOpenedApplicationLogExceptionInterceptor = true;
            Engine.Configuration.Settings.IsOpenedCachingInterceptor = true;

            UPrimeEngine.Instance.Register<OneRedisCacheDatabaseProvider, OneRedisCacheDatabaseProvider>();
            UPrimeEngine.Instance.Register<ZeroRedisCacheDatabaseProvider, ZeroRedisCacheDatabaseProvider>();

            UPrimeEngine.Instance.Register<ICacheManager, ZeroCacheManager>(U.Dependency.DependencyLifeStyle.Singleton, "zero");
            UPrimeEngine.Instance.Register<ICacheManager, OneCacheManager>(U.Dependency.DependencyLifeStyle.Singleton, "one");
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Engine.Configuration.BackgroundJob.UseHangfire();
            //Engine.Configuration.Caching.UseRedis();

            
        }

    }
}