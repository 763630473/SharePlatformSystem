namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    ///定义后台作业的接口。
    /// </summary>
    public interface IBackgroundJob<in TArgs>
    {
        /// <summary>
        /// 使用“args”执行作业。
        /// </summary>
        /// <param name="args">工作参数。</param>
        void Execute(TArgs args);
    }
}