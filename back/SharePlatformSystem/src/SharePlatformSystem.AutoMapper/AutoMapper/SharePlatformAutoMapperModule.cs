using System;
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Core.Configuration.Startup;

namespace SharePlatformSystem.AutoMapper
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformAutoMapperModule : SharePlatformModule
    {
        private readonly ITypeFinder _typeFinder;

        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();

        public SharePlatformAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<ISharePlatformAutoMapperConfiguration, SharePlatformAutoMapperConfiguration>();
            
            Configuration.ReplaceService<Core.ObjectMapping.IObjectMapper, AutoMapperObjectMapper>();

            Configuration.Modules.SharePlatformAutoMapper().Configurators.Add(CreateCoreMappings);
        }

        public override void PostInitialize()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.Modules.SharePlatformAutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                };

                if (Configuration.Modules.SharePlatformAutoMapper().UseStaticMapper)
                {
                    //我们应该防止应用程序中的重复映射，因为映射器是静态的。
                    if (!_createdMappingsBefore)
                    {
                        Mapper.Initialize(configurer);
                        _createdMappingsBefore = true;
                    }

                    IocManager.IocContainer.Register(
                        Component.For<IConfigurationProvider>().Instance(Mapper.Configuration).LifestyleSingleton()
                    );
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(Mapper.Instance).LifestyleSingleton()
                    );
                }
                else
                {
                    var config = new MapperConfiguration(configurer);
                    IocManager.IocContainer.Register(
                        Component.For<IConfigurationProvider>().Instance(config).LifestyleSingleton()
                    );
                    IocManager.IocContainer.Register(
                        Component.For<IMapper>().Instance(config.CreateMapper()).LifestyleSingleton()
                    );
                }
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                           typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                           typeInfo.IsDefined(typeof(AutoMapToAttribute));
                }
            );

            Logger.DebugFormat("Found {0} classes define auto mapping attributes", types.Length);

            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }

        private void CreateCoreMappings(IMapperConfigurationExpression configuration)
        {
            var localizationContext = IocManager.Resolve<ILocalizationContext>();

            configuration.CreateMap<ILocalizableString, string>().ConvertUsing(ls => ls == null ? null : ls.Localize(localizationContext));
            configuration.CreateMap<LocalizableString, string>().ConvertUsing(ls => ls == null ? null : localizationContext.LocalizationManager.GetString(ls));
        }
    }
}
