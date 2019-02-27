﻿using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Quartz;

namespace SharePlatformSystem.Quartz
{
    public class SharePlatformQuartzJobListener : IJobListener
    {
        public string Name { get; } = "AbpJobListener";

        public ILogger Logger { get; set; }

        public SharePlatformQuartzJobListener()
        {
            Logger = NullLogger.Instance;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.Debug($"Job {context.JobDetail.JobType.Name} executing...");
            return Task.FromResult(0);
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.Info($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
            return Task.FromResult(0);
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobException == null)
            {
                Logger.Debug($"Job {context.JobDetail.JobType.Name} sucessfully executed.");
            }
            else
            {
                Logger.Error($"Job {context.JobDetail.JobType.Name} failed with exception: {jobException}");
            }
            return Task.FromResult(0);
        }
    }
}