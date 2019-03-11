using SharePlatformSystem.Core.Configuration;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Localization
{
    public class LocalizationSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage, null, L("DefaultLanguage"), scopes: SettingScopes.Application |  SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider())
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, SharePlatformConsts.LocalizationSourceName);
        }
    }
}