using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Runtime.Caching.Configuration
{
    public class CachingConfiguration : ICachingConfiguration
    {
        public ISharePlatformStartupConfiguration SharePlatformConfiguration { get; private set; }

        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }
        private readonly List<ICacheConfigurator> _configurators;

        public CachingConfiguration(ISharePlatformStartupConfiguration sharePlatformConfiguration)
        {
            SharePlatformConfiguration = sharePlatformConfiguration;

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