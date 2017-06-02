
namespace U.Startup.Configuration
{
    internal class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }

        public IUStartupConfiguration UConfiguration { get; private set; }

        public BackgroundJobConfiguration(IUStartupConfiguration configuration)
        {
            UConfiguration = configuration;

            IsJobExecutionEnabled = false;
        }
    }
}
