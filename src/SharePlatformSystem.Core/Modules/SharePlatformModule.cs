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
    /// This class must be implemented by all module definition classes.
    /// </summary>
    /// <remarks>
    /// A module definition class is generally located in its own assembly
    /// and implements some action in module events on application startup and shutdown.
    /// It also defines depended modules.
    /// </remarks>
    public abstract class SharePlatformModule
    {
        /// <summary>
        /// Gets a reference to the IOC manager.
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// Gets a reference to the SharePlatform configuration.
        /// </summary>
        protected internal ISharePlatformStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        protected SharePlatformModule()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// This is the first event called on application startup.
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// This method is called lastly on application startup.
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// This method is called when the application is being shutdown.
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        /// <summary>
        /// Checks if given type is an SharePlatform module class.
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
        /// Finds direct depended modules of a module (excluding given module).
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
                throw new SharePlatformInitializationException("This type is not an SharePlatform module: " + module.AssemblyQualifiedName);
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
