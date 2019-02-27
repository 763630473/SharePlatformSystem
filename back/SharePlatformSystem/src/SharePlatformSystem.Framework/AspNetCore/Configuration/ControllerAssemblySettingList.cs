using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SharePlatformSystem.Core.Reflection.Extensions;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public class ControllerAssemblySettingList : List<SharePlatformControllerAssemblySetting>
    {
        public List<SharePlatformControllerAssemblySetting> GetSettings(Type controllerType)
        {
            return this.Where(controllerSetting => controllerSetting.Assembly == controllerType.GetAssembly()).ToList();
        }
    }
}