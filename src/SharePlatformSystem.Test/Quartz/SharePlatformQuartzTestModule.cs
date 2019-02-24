using System.Reflection;
using Abp.Modules;

namespace SharePlatformSystem.AutoMapper.Tests
{
    [DependsOn(typeof(AbpQuartzModule))]
    public class SharePlatformQuartzTestModule : AbpModule
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
