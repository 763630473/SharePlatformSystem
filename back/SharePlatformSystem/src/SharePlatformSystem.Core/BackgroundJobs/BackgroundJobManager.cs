using System;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus;
using SharePlatformSystem.Events.Bus.Exceptions;
using SharePlatformSystem.Json;
using SharePlatformSystem.Threading;
using SharePlatformSystem.Threading.BackgroundWorkers;
using SharePlatformSystem.Threading.Timers;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// “IBackgroundJobManager”的默认实现。
    /// </summary>
    public class BackgroundJobManager : PeriodicBackgroundWorkerBase, IBackgroundJobManager, ISingletonDependency
    {
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 从“ibackgroundjobstore”轮询作业之间的间隔。
        /// 默认值: 5000 (5 秒).
        /// </summary>
        public static int JobPollPeriod { get; set; }

        private readonly IIocResolver _iocResolver;
        private readonly IBackgroundJobStore _store;

        static BackgroundJobManager()
        {
            JobPollPeriod = 5000;
        }

        /// <summary>
        /// 初始化“BackgroundJobManager”类的新实例。
        /// </summary>
        public BackgroundJobManager(
            IIocResolver iocResolver,
            IBackgroundJobStore store,
            SharePlatformTimer timer)
            : base(timer)
        {
            _store = store;
            _iocResolver = iocResolver;

            EventBus = NullEventBus.Instance;

            Timer.Period = JobPollPeriod;
        }

        public async Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = typeof(TJob).AssemblyQualifiedName,
                JobArgs = args.ToJsonString(),
                Priority = priority
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            await _store.InsertAsync(jobInfo);

            return jobInfo.Id.ToString();
        }

        public async Task<bool> DeleteAsync(string jobId)
        {        
            BackgroundJobInfo jobInfo = await _store.GetAsync(jobId);
            if (jobInfo == null)
            {
                return false;
            }

            await _store.DeleteAsync(jobInfo);
            return true;
        }

        protected override void DoWork()
        {
            var waitingJobs = AsyncHelper.RunSync(() => _store.GetWaitingJobsAsync(1000));

            foreach (var job in waitingJobs)
            {
                TryProcessJob(job);
            }
        }

        private void TryProcessJob(BackgroundJobInfo jobInfo)
        {
            try
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = Clock.Now;

                var jobType = Type.GetType(jobInfo.JobType);
                using (var job = _iocResolver.ResolveAsDisposable(jobType))
                {
                    try
                    {
                        var jobExecuteMethod = job.Object.GetType().GetTypeInfo().GetMethod("Execute");
                        var argsType = jobExecuteMethod.GetParameters()[0].ParameterType;
                        var argsObj = JsonConvert.DeserializeObject(jobInfo.JobArgs, argsType);

                        jobExecuteMethod.Invoke(job.Object, new[] { argsObj });

                        AsyncHelper.RunSync(() => _store.DeleteAsync(jobInfo));
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.Message, ex);

                        var nextTryTime = jobInfo.CalculateNextTryTime();
                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        TryUpdate(jobInfo);

                        EventBus.Trigger(
                            this,
                            new SharePlatformHandledExceptionData(
                                new BackgroundJobException(
                                    "后台作业执行失败。有关详细信息，请参阅内部异常。请参阅BackgroundJob属性以获取有关后台作业的信息。", ex)
                                {
                                    BackgroundJob = jobInfo,
                                    JobObject = job.Object
                                }
                            )
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);

                jobInfo.IsAbandoned = true;

                TryUpdate(jobInfo);
            }
        }

        private void TryUpdate(BackgroundJobInfo jobInfo)
        {
            try
            {
                _store.UpdateAsync(jobInfo);
            }
            catch (Exception updateEx)
            {
                Logger.Warn(updateEx.ToString(), updateEx);
            }
        }
    }
}
