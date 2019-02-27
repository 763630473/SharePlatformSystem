using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public ISharePlatformStartupConfiguration SharePlatformConfiguration { get; private set; }

        public ModuleConfigurations(ISharePlatformStartupConfiguration sharePlatformConfiguration)
        {
            SharePlatformConfiguration = sharePlatformConfiguration;
        }
    }
}