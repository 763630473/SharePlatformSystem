using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Configuration
{
    public class CustomConfigProviderContext
    {
        public IScopedIocResolver IocResolver { get; }

        public CustomConfigProviderContext(IScopedIocResolver iocResolver)
        {
            IocResolver = iocResolver;
        }
    }
}