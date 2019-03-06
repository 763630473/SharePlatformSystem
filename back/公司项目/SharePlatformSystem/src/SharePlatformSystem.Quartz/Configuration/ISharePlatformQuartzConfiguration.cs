using Quartz;

namespace SharePlatformSystem.Quartz.Configuration
{
    public interface ISharePlatformQuartzConfiguration
    {
        IScheduler Scheduler { get;}
    }
}