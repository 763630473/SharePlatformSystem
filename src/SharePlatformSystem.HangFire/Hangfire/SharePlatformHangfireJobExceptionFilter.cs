using Hangfire.Common;
using Hangfire.Server;
using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus;
using SharePlatformSystem.Events.Bus.Exceptions;

namespace SharePlatformSystem.HangFire
{
    public class SharePlatformHangfireJobExceptionFilter : JobFilterAttribute, IServerFilter, ITransientDependency
    {
        public IEventBus EventBus { get; set; }

        public SharePlatformHangfireJobExceptionFilter()
        {
            EventBus = NullEventBus.Instance;
        }

        public void OnPerforming(PerformingContext filterContext)
        {
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                EventBus.Trigger(
                    this,
                    new SharePlatformHandledExceptionData(
                        new BackgroundJobException(
                            "A background job execution is failed on Hangfire. See inner exception for details. Use JobObject to get Hangfire background job object.",
                            filterContext.Exception
                        )
                        {
                            JobObject = filterContext.BackgroundJob
                        }
                    )
                );
            }
        }
    }
}
