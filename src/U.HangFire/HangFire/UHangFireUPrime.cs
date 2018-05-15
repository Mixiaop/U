using Hangfire;
using System.Reflection;
using U.BackgroundJobs;
using U.UPrimes;
using U.Hangfire.Startup.Configuration;

namespace U.Hangfire
{
    [DependsOn(typeof(ULeadershipUPrime))]
    public class UHangfireUPrime : U.UPrimes.UPrime
    {
        public override void PreInitialize()
        {
            Engine.Register<IUHangfireConfiguration, UHangfireConfiguration>();
            try
            {
                var config = Engine.Resolve<IUHangfireConfiguration>();
                config.GlobalConfiguration.UseActivator(new HangfireIocJobActivator(Engine.IocManager));
                config.GlobalConfiguration.UseSqlServerStorage(config.SqlServerConn);

            }
            catch (UException ex) {
                
            }
        }
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            
        }
    }
}
