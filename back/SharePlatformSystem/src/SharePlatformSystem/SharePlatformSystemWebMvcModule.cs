using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharePlatformSystem.Auth.App.Interface;
using SharePlatformSystem.Auth.App.SSO;
using SharePlatformSystem.Auth.EfRepository;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Framework.AspNetCore;
using System.Reflection;


namespace SharePlatformSystem.Web
{
    [DependsOn(
        typeof(SharePlatformKernelModule),
        typeof(SharePlatformSystemInfrastructureModule),
        typeof(SharePlatformSystemAuthAppModule),
        typeof(SharePlatformAspNetCoreModule),
          typeof(SharePlatformAspNetCoreModule)
        )]
    public class SharePlatformSystemWebMvcModule : SharePlatformModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;
        public SharePlatformSystemWebMvcModule(
    IHostingEnvironment env)
        {
            _env = env;           
        }

        public override void PreInitialize()
        {
            //IocManager.Register<IAuth, LocalAuth>();
            var builder = new DbContextOptionsBuilder<SharePlatformDBContext>();

         
        
           
            //Configuration.ReplaceService<IConnectionStringResolver, BidConnectionStringResolver>();
            Configuration.UnitOfWork.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;            
        }

        public override void Initialize()
        {      
            
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());         
        }
       
        public override void PostInitialize()
        {       
            var workManager = IocManager.Resolve<Threading.BackgroundWorkers.IBackgroundWorkerManager>();         
        }
    }
}
