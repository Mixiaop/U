using U.Configuration.Startup;

namespace U.BackgroundJobs
{
    public class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }

        public IUStartupConfiguration Configuration { get; set; }

        public BackgroundJobConfiguration(IUStartupConfiguration configuration) {
            Configuration = configuration;
            IsJobExecutionEnabled = true;
        }
    }
}
