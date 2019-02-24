using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Quartz;
using System.Reflection;

namespace SharePlatformSystem.AutoMapper.Tests
{
    [DependsOn(typeof(SharePlatformQuartzModule))]
    public class SharePlatformQuartzTestModule : SharePlatformModule
    {
        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
