using System.Reflection;
using Quartz;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Quartz.Configuration;
using SharePlatformSystem.Threading.BackgroundWorkers;

namespace SharePlatformSystem.Quartz
{
    [DependsOn(typeof (SharePlatformKernelModule))]
    public class SharePlatformQuartzModule : SharePlatformModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ISharePlatformQuartzConfiguration, SharePlatformQuartzConfiguration>();
            var c = Configuration.Modules.SharePlatformQuartz();
            var s = c.Scheduler;
                s.JobFactory = new SharePlatformQuartzJobFactory(IocManager);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IJobListener, SharePlatformQuartzJobListener>();

            Configuration.Modules.SharePlatformQuartz().Scheduler.ListenerManager.AddJobListener(IocManager.Resolve<IJobListener>());

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.Resolve<IBackgroundWorkerManager>().Add(IocManager.Resolve<IQuartzScheduleJobManager>());
            }
        }
    }
}
