using SharePlatformSystem.Core.Configuration;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Timing
{
    public class TimingSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(TimingSettingNames.TimeZone, "UTC", L("TimeZone"), scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User)
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, SharePlatformConsts.LocalizationSourceName);
        }
    }
}
