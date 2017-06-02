using U.Dependency;
using U.Startup.Configuration;
using U.BackgroundJobs;

namespace U.Hangfire
{
    public static class UHangfireUPrimeConfigurationExtensions
    {
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration) {

            backgroundJobConfiguration.UConfiguration.Engine.IocManager.RegisterIfNot<IBackgroundJobManager, HangfireBackgroundJobManager>();
        }
    }
}
