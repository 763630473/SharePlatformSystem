﻿using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Localization;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Timing
{
    public class TimingSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(TimingSettingNames.TimeZone, "UTC", L("TimeZone"), scopes: SettingScopes.Application | SettingScopes.User)
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, SharePlatformConsts.LocalizationSourceName);
        }
    }
}
