using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public class SharePlatformControllerAssemblySettingBuilder : ISharePlatformControllerAssemblySettingBuilder
    {
        private readonly SharePlatformControllerAssemblySetting _setting;

        public SharePlatformControllerAssemblySettingBuilder(SharePlatformControllerAssemblySetting setting)
        {
            _setting = setting;
        }

        public SharePlatformControllerAssemblySettingBuilder Where(Func<Type, bool> predicate)
        {
            _setting.TypePredicate = predicate;
            return this;
        }

        public SharePlatformControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer)
        {
            _setting.ControllerModelConfigurer = configurer;
            return this;
        }
    }
}