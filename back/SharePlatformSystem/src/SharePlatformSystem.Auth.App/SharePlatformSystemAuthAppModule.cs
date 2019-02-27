using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Auth.App.SSO;
using SharePlatformSystem.Auth.EfRepository;
using SharePlatformSystem.Auth.EfRepository.Interface;
using SharePlatformSystem.Core.Modules;
using System.Reflection;


namespace SharePlatformSystem.Web
{
    [DependsOn(typeof(Core.SharePlatformKernelModule),typeof(SharePlatformSystemAuthEfRepositoryModule))]
    public class SharePlatformSystemAuthAppModule :SharePlatformModule
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
