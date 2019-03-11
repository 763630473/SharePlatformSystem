using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.PlugIns;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.EntityHistory;
using SharePlatformSystem.Runtime.Caching.Configuration;

namespace SharePlatformSystem.Dependency.Installers
{
    internal class SharePlatformCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
                Component.For<ILocalizationConfiguration, LocalizationConfiguration>().ImplementedBy<LocalizationConfiguration>().LifestyleSingleton(),
                Component.For<ISettingsConfiguration, SettingsConfiguration>().ImplementedBy<SettingsConfiguration>().LifestyleSingleton(),
                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
                Component.For<IEventBusConfiguration, EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
                Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
                Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
                Component.For<IBackgroundJobConfiguration, BackgroundJobConfiguration>().ImplementedBy<BackgroundJobConfiguration>().LifestyleSingleton(),
                 Component.For<Core.Resources.Embedded.IEmbeddedResourcesConfiguration, Core.Resources.Embedded.EmbeddedResourcesConfiguration>().ImplementedBy<Core.Resources.Embedded.EmbeddedResourcesConfiguration>().LifestyleSingleton(),
                Component.For<ISharePlatformStartupConfiguration, SharePlatformStartupConfiguration>().ImplementedBy<SharePlatformStartupConfiguration>().LifestyleSingleton(),
                Component.For<IEntityHistoryConfiguration, EntityHistoryConfiguration>().ImplementedBy<EntityHistoryConfiguration>().LifestyleSingleton(),
                Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<ISharePlatformPlugInManager, SharePlatformPlugInManager>().ImplementedBy<SharePlatformPlugInManager>().LifestyleSingleton(),
                Component.For<ISharePlatformModuleManager, SharePlatformModuleManager>().ImplementedBy<SharePlatformModuleManager>().LifestyleSingleton(),
                Component.For<IAssemblyFinder, SharePlatformAssemblyFinder>().ImplementedBy<SharePlatformAssemblyFinder>().LifestyleSingleton(),
                Component.For<ILocalizationManager, LocalizationManager>().ImplementedBy<LocalizationManager>().LifestyleSingleton()
                );
        }
    }
}
