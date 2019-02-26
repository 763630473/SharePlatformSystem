using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;


namespace SharePlatformSystem.Web
{
    [Core.Modules.DependsOn(
        typeof(Core.SharePlatformKernelModule),
        typeof(Framework.AspNetCore.SharePlatformAspNetCoreModule)
        )]
    public class SharePlatformSystemWebMvcModule : Core.Modules.SharePlatformModule
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
