using System;
using System.Collections.Generic;
using U.Configuration;
using U.Runtime.Caching;
using U.Startup.Configuration;

namespace U.Startup
{
    /// <summary>
    /// Used to configure Modules on startup
    /// U模块的启动配置
    /// </summary>
    public partial interface IUStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// UPrime engine.
        /// </summary>
        UPrimeEngine Engine { get; }

        /// <summary>
        /// used to configure settings.
        /// </summary>
        ISettingsConfiguration Settings { get; }

        /// <summary>
        /// used to configure logging.
        /// </summary>
        ILoggingConfiguration Logging { get; }

        /// <summary>
        /// used to configure caching.
        /// </summary>
        ICachingConfiguration Caching { get; }

        /// <summary>
        /// used to configure background job system.
        /// </summary>
        IBackgroundJobConfiguration BackgroundJob { get; }

        void Initialize();
    }
}
