using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Modules
{
    public interface ISharePlatformModuleManager
    {
        SharePlatformModuleInfo StartupModule { get; }

        IReadOnlyList<SharePlatformModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}