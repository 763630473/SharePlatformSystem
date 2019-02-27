using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public interface ISharePlatformControllerAssemblySettingBuilder
    {
        SharePlatformControllerAssemblySettingBuilder Where(Func<Type, bool> predicate);

        SharePlatformControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer);
    }
}