using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// 表示用于持久化作业的后台作业信息。
    /// </summary>
    [Table("BackgroundJobs")]
    public class BackgroundJobInfo : CreationAuditedEntity<string>
    {
        /// <summary>
        /// "JobType"的最大长度.
        /// Value: 512.
        /// </summary>
        public const int MaxJobTypeLength = 512;

        /// <summary>
        ///"JobArgs"的最大长度.
        /// Value: 1 MB (1,048,576 bytes).
        /// </summary>
        public const int MaxJobArgsLength = 1024 * 1024;

        /// <summary>
        /// 失败时第一次等待的默认持续时间（秒）。
        /// 默认值: 60 (1 分钟).
        /// </summary>
        public static int DefaultFirstWaitDuration { get; set; }

        /// <summary>
        /// 作业放弃“isabandoned”之前的默认超时值（秒）。
        /// 默认值: 172,800 (2 天).
        /// </summary>
        public static int DefaultTimeout { get; set; }

        /// <summary>
        ///执行失败的默认等待因子。
        /// 此金额乘以最后等待时间，以计算下一等待时间。
        /// 默认值: 2.0.
        /// </summary>
        public static double DefaultWaitFactor { get; set; }

        /// <summary>
        /// 作业的类型。
        /// 它是作业类型的assemblyQualifiedName。
        /// </summary>
        [Required]
        [StringLength(MaxJobTypeLength)]
        public virtual string JobType { get; set; }

        /// <summary>
        /// 作为JSON字符串的作业参数。
        /// </summary>
        [Required]
        [StringLength(MaxJobArgsLength)]
        public virtual string JobArgs { get; set; }

        /// <summary>
        ///尝试此作业的计数。
        ///如果作业失败，将重新尝试。
        /// </summary>
        public virtual short TryCount { get; set; }

        /// <summary>
        /// 此作业的下一次尝试时间。
        /// </summary>
        //[Index("IX_IsAbandoned_NextTryTime", 2)]
        public virtual DateTime NextTryTime { get; set; }

        /// <summary>
        ///此作业的上次尝试时间。
        /// </summary>
        public virtual DateTime? LastTryTime { get; set; }

        /// <summary>
        ///如果此作业连续失败且不会再次执行，则为真。
        /// </summary>
        //[Index("IX_IsAbandoned_NextTryTime", 1)]
        public virtual bool IsAbandoned { get; set; }

        /// <summary>
        ///此工作的优先级。
        /// </summary>
        public virtual BackgroundJobPriority Priority { get; set; }

        static BackgroundJobInfo()
        {
            DefaultFirstWaitDuration = 60;
            DefaultTimeout = 172800;
            DefaultWaitFactor = 2.0;
        }

        /// <summary>
        /// 初始化“BackgroundJobInfo”类的新实例。
        /// </summary>
        public BackgroundJobInfo()
        {
            NextTryTime = Clock.Now;
            Priority = BackgroundJobPriority.Normal;
        }

        /// <summary>
        /// 如果作业失败，则计算下一次尝试时间。
        /// 如果它不再等待并且应该放弃作业，则返回空值。
        /// </summary>
        /// <returns></returns>
        public virtual DateTime? CalculateNextTryTime()
        {
            var nextWaitDuration = DefaultFirstWaitDuration * (Math.Pow(DefaultWaitFactor, TryCount - 1));
            var nextTryDate = LastTryTime.HasValue
                ? LastTryTime.Value.AddSeconds(nextWaitDuration)
                : Clock.Now.AddSeconds(nextWaitDuration);

            if (nextTryDate.Subtract(CreationTime).TotalSeconds > DefaultTimeout)
            {
                return null;
            }

            return nextTryDate;
        }
    }
}