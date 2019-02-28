using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SharePlatformSystem.Auth.EfRepository;
using SharePlatformSystem.Auth.EfRepository.Interface;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using System.Reflection;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Collections.Extensions;
using SharePlatformSystem.Core.Orm;
using System;

namespace SharePlatformSystem.Web
{
    [DependsOn(typeof(SharePlatformKernelModule),typeof(SharePlatformSystemInfrastructureModule))]
    public class SharePlatformSystemAuthEfRepositoryModule :SharePlatformModule
    {
        public bool SkipDbContextRegistration { get; set; }
        public override void PreInitialize()
        {
            IocManager.Register<ISharePlatformEfCoreConfiguration, SharePlatformEfCoreConfiguration>();
           
           // IocManager.RegisterIfNot<IUnitWork, UnitWork>();
           // IocManager.IocContainer.Register(
           //    Component.For(typeof(IRepository<>)).ImplementedBy(typeof(BaseRepository<>))
           //);
            // IocManager.Register<IUnitWork, UnitWork>(Dependency.DependencyLifeStyle.Transient);

        }

        public override void Initialize()
        {      
            
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());


           
        }
      
        public override void PostInitialize()
        {       
            
        }
    }
}
