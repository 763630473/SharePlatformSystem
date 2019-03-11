using SharePlatformSystem.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem.Core.Modules
{
    /// <summary>
    //���ڽ�SharePlatformModuleInfo����洢Ϊ�ֵ䡣
    /// </summary>
    internal class SharePlatformModuleCollection : List<SharePlatformModuleInfo>
    {
        public Type StartupModuleType { get; }

        public SharePlatformModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        /// <summary>
        /// ��ȡ��ģ��ʵ�������á�
        /// </summary>
        /// <typeparam name="TModule">ģ������</typeparam>
        /// <returns>����ģ��ʵ��</returns>
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
        /// �����������ģ������
        ///���ģ��A������ģ��B���򷵻��б���A��B֮��
        /// </summary>
        /// <returns>�����</returns>
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
                //�Ѿ��ǵ�һ���ˣ�
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
                //�Ѿ������һ���ˣ�
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