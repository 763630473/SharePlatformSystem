using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.NHRepository;
using System.Reflection;


namespace SharePlatformSystem.NetHouse.App
{
    [DependsOn(typeof(SharePlatformKernelModule),typeof(SharePlatformSystemNHRepositoryModule))]
    public class SharePlatformSystemNetHouseAppModule :SharePlatformModule
    {
       
           
       
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
