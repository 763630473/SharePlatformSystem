using SharePlatformSystem.Events.Bus;
using System;
using System.Threading.Tasks;

namespace SharePlatformSystem.BackgroundJobs
{
    public static class BackgroundJobManagerEventTriggerExtensions
    {
        public static Task EnqueueEventAsync<TEvent>(this IBackgroundJobManager backgroundJobManager,
            TEvent e,BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) where TEvent:EventData
        {
            return backgroundJobManager.EnqueueAsync<EventTriggerAsyncBackgroundJob<TEvent>,TEvent>(e,priority,delay);
        }
    }
}