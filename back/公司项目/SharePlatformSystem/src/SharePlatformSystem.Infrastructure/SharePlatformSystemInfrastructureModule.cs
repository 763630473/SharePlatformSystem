using Microsoft.Extensions.Configuration;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Infrastructure.Cache;
using System.Reflection;


namespace SharePlatformSystem.Web
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformSystemInfrastructureModule : SharePlatformModule
    {        
        public override void PreInitialize()
        {
            IocManager.Register<ICacheContext, CacheContext>();
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
