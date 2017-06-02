using Hangfire;
using HangfireGlobalConfiguration = Hangfire.GlobalConfiguration;

namespace U.Hangfire.Startup.Configuration
{
    public class UHangfireConfiguration : IUHangfireConfiguration
    {
        public UHangfireConfiguration(HangfireSettings settings) {
            SqlServerConn = settings.SqlServerConn;   
        }
        public BackgroundJobServer Server { get; set; }

        public IGlobalConfiguration GlobalConfiguration
        {
            get { return HangfireGlobalConfiguration.Configuration; }
        }



        public string SqlServerConn
        {
            get;
            private set;
        }
    }
}
