using System.Collections.Generic;

namespace SharePlatformSystem.Core.Configuration
{
    public interface ICustomConfigProvider
    {
        Dictionary<string, object> GetConfig(CustomConfigProviderContext customConfigProviderContext);
    }
}
