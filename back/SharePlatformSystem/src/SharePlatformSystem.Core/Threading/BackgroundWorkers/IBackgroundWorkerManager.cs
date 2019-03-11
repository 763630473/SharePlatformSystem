namespace SharePlatformSystem.Threading.BackgroundWorkers
{
    /// <summary>
    /// 用于管理后台工作人员。
    /// </summary>
    public interface IBackgroundWorkerManager : IRunnable
    {
        /// <summary>
        /// 添加新worker。如果启动了<see cref=“ibackgroundWorkerManager”/>，则立即启动worker。
        /// </summary>
        /// <param name="worker">
        /// The worker. It should be resolved from IOC.
        /// </param>
        void Add(IBackgroundWorker worker);
    }
}