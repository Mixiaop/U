using U.Configuration;
using U.Startup.Configuration;

namespace U.Startup
{
    public partial class UStartupConfiguration : DictionaryBasedConfig, IUStartupConfiguration
    {
        public UPrimeEngine Engine { get; private set; }

        /// <summary>
        /// used to configure settings.
        /// </summary>
        public ISettingsConfiguration Settings { get; private set; }

        /// <summary>
        /// used to configure logging.
        /// </summary>
        public ILoggingConfiguration Logging { get; private set; }

        /// <summary>
        /// used to configure caching.
        /// </summary>
        public ICachingConfiguration Caching { get; private set; }

        /// <summary>
        /// used to configure background job system.
        /// </summary>
        public IBackgroundJobConfiguration BackgroundJob { get; private set; }

        public void Initialize()
        {
            Engine = UPrimeEngine.Instance;
            Settings = Engine.Resolve<ISettingsConfiguration>();
            Logging = Engine.Resolve<ILoggingConfiguration>();
            Caching = Engine.Resolve<ICachingConfiguration>();
            BackgroundJob = Engine.Resolve<IBackgroundJobConfiguration>();
        }
    }
}
