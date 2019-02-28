using System;
using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using JetBrains.Annotations;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.PlugIns;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Dependency.Installers;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.EntityHistory;

namespace SharePlatformSystem
{
    /// <summary>
    /// This is the main class that is responsible to start entire SharePlatform system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class SharePlatformBootstrapper : IDisposable
    {
        /// <summary>
        /// Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// A list of plug in folders.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private SharePlatformModuleManager _moduleManager;
        private ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="SharePlatformBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="SharePlatformModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        private SharePlatformBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<SharePlatformBootstrapperOptions> optionsAction = null)
        {
            Check.NotNull(startupModule, nameof(startupModule));

            var options = new SharePlatformBootstrapperOptions();
            optionsAction?.Invoke(options);

            if (!typeof(SharePlatformModule).GetTypeInfo().IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(SharePlatformModule)}.");
            }

            StartupModule = startupModule;

            IocManager = options.IocManager;
            PlugInSources = options.PlugInSources;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="SharePlatformModule"/>.</typeparam>
        /// <param name="optionsAction">An action to set options</param>
        public static SharePlatformBootstrapper Create<TStartupModule>([CanBeNull] Action<SharePlatformBootstrapperOptions> optionsAction = null)
            where TStartupModule : SharePlatformModule
        {
            return new SharePlatformBootstrapper(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="SharePlatformModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        public static SharePlatformBootstrapper Create([NotNull] Type startupModule, [CanBeNull] Action<SharePlatformBootstrapperOptions> optionsAction = null)
        {
            return new SharePlatformBootstrapper(startupModule, optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="SharePlatformModule"/>.</typeparam>
        /// <param name="iocManager">IIocManager that is used to bootstrap the SharePlatform system</param>
        [Obsolete("Use overload with parameter type: Action<SharePlatformBootstrapperOptions> optionsAction")]
        public static SharePlatformBootstrapper Create<TStartupModule>([NotNull] IIocManager iocManager)
            where TStartupModule : SharePlatformModule
        {
            return new SharePlatformBootstrapper(typeof(TStartupModule), options =>
            {
                options.IocManager = iocManager;
            });
        }

        /// <summary>
        /// Creates a new <see cref="SharePlatformBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="SharePlatformModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the SharePlatform system</param>
        [Obsolete("Use overload with parameter type: Action<SharePlatformBootstrapperOptions> optionsAction")]
        public static SharePlatformBootstrapper Create([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            return new SharePlatformBootstrapper(startupModule, options =>
            {
                options.IocManager = iocManager;
            });
        }

        private void AddInterceptorRegistrars()
        {    
            AuditingInterceptorRegistrar.Initialize(IocManager);
            EntityHistoryInterceptorRegistrar.Initialize(IocManager);
            //UnitOfWorkRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// Initializes the SharePlatform system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new SharePlatformCoreInstaller());

                IocManager.Resolve<SharePlatformPlugInManager>().PlugInSources.AddRange(PlugInSources);
                IocManager.Resolve<SharePlatformStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<SharePlatformModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(SharePlatformBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<SharePlatformBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<SharePlatformBootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// Disposes the SharePlatform system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
