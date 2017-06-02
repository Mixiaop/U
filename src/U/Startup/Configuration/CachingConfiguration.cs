using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using U.Runtime.Caching;

namespace U.Startup.Configuration
{
    public class CachingConfiguration : ICachingConfiguration
    {
        public IUStartupConfiguration UConfiguration { get; private set; }

        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }
        private readonly List<ICacheConfigurator> _configurators;

        public CachingConfiguration(IUStartupConfiguration configuration)
        {
            UConfiguration = configuration;

            _configurators = new List<ICacheConfigurator>();
        }

        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}
