using SharePlatformSystem.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem.Core.PlugIns
{
    public static class PlugInSourceExtensions
    {
        public static List<Type> GetModulesWithAllDependencies(this IPlugInSource plugInSource)
        {
            return plugInSource
                .GetModules()
                .SelectMany(SharePlatformModule.FindDependedModuleTypesRecursivelyIncludingGivenModule)
                .Distinct()
                .ToList();
        }
    }
}