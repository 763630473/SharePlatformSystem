namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// 后台作业的优先级。
    /// </summary>
    public enum BackgroundJobPriority : byte
    {
        /// <summary>
        /// 低.
        /// </summary>
        Low = 5,

        /// <summary>
        /// 低于正常值。
        /// </summary>
        BelowNormal = 10,

        /// <summary>
        ///正常（默认）。
        /// </summary>
        Normal = 15,

        /// <summary>
        /// 正常以上。
        /// </summary>
        AboveNormal = 20,

        /// <summary>
        /// 高。
        /// </summary>
        High = 25
    }
}