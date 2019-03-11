using Castle.Core.Logging;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharePlatformSystem.Core.Modules
{
    /// <summary>
    ///此类必须由所有模块定义类实现。
    ///</summary>
    ///<remarks>
    ///模块定义类通常位于自己的程序集中
    ///并在应用程序启动和关闭时在模块事件中实现一些操作。
    ///它还定义了依赖的模块。
    /// </remarks>
    public abstract class SharePlatformModule
    {
        /// <summary>
        /// 获取对IOC manager的引用。
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// 获取对SharePlatform配置的引用。
        /// </summary>
        protected internal ISharePlatformStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// 获取或设置记录器。
        /// </summary>
        public ILogger Logger { get; set; }

        protected SharePlatformModule()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        ///这是应用程序启动时调用的第一个事件。
        ///可以将代码放在此处，以便在依赖项注入注册之前运行。
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 此方法用于注册此模块的依赖项。
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 此方法在应用程序启动时最后调用。
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 当应用程序正在关闭时调用此方法。
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        /// <summary>
        ///检查给定类型是否为SharePlatform模块类。
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsSharePlatformModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(SharePlatformModule).IsAssignableFrom(type);
        }

        /// <summary>
        ///查找模块（不包括给定模块）的直接相关模块。
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsSharePlatformModule(moduleType))
            {
                throw new SharePlatformInitializationException("此类型不是SharePlatform模块: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(SharePlatformKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsSharePlatformModule(module))
            {
                throw new SharePlatformInitializationException("此类型不是SharePlatform模块: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }
    }
}
