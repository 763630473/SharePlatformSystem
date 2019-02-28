using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using System.Reflection;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Collections.Extensions;
using SharePlatformSystem.Core.Orm;
using System;

namespace SharePlatformSystem.NHRepository
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformSystemNHRepositoryModule :SharePlatformModule
    {
        public bool SkipDbContextRegistration { get; set; }
        public override void PreInitialize()
        {


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
