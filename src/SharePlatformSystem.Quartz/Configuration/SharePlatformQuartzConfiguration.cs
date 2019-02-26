using Quartz;
using Quartz.Impl;

namespace SharePlatformSystem.Quartz.Configuration
{
    public class SharePlatformQuartzConfiguration : ISharePlatformQuartzConfiguration
    {
        public IScheduler Scheduler => StdSchedulerFactory.GetDefaultScheduler().Result;
    }
}