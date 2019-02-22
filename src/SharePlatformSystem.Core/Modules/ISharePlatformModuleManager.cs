using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Modules
{
    public interface ISharePlatformModuleManager
    {
        AbpModuleInfo StartupModule { get; }

        IReadOnlyList<AbpModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}