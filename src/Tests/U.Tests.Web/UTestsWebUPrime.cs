using System.Reflection;
using System.Net;
using U.UPrimes;
using U.Hangfire;
using U.Runtime.Caching.Redis;
using U.Dapper;
using U.EntityFramework;
using U.Utilities.Web;
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
    public class UTestsWebUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.Configuration.BackgroundJob.IsJobExecutionEnabled = true;
            var a = 1;
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Engine.Configuration.BackgroundJob.UseHangfire();
            //Engine.Configuration.Caching.UseRedis();

            
        }

    }
}