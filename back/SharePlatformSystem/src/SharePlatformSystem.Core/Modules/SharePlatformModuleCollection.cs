using SharePlatformSystem.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem.Core.Modules
{
    /// <summary>
    //用于将SharePlatformModuleInfo对象存储为字典。
    /// </summary>
    internal class SharePlatformModuleCollection : List<SharePlatformModuleInfo>
    {
        public Type StartupModuleType { get; }

        public SharePlatformModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        /// <summary>
        /// 获取对模块实例的引用。
        /// </summary>
        /// <typeparam name="TModule">模块类型</typeparam>
        /// <returns>引用模块实例</returns>
        public TModule GetModule<TModule>() where TModule : SharePlatformModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new SharePlatformException("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

        /// <summary>
        /// 根据依赖项对模块排序。
        ///如果模块A依赖于模块B，则返回列表中A在B之后。
        /// </summary>
        /// <returns>排序表</returns>
        public List<SharePlatformModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, StartupModuleType);
            return sortedModules;
        }

        public static void EnsureKernelModuleToBeFirst(List<SharePlatformModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(SharePlatformKernelModule));
            if (kernelModuleIndex <= 0)
            {
                //已经是第一次了！
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        public static void EnsureStartupModuleToBeLast(List<SharePlatformModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
            {
                //已经是最后一个了！
                return;
            }

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        public void EnsureKernelModuleToBeFirst()
        {
            EnsureKernelModuleToBeFirst(this);
        }

        public void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }
    }
}