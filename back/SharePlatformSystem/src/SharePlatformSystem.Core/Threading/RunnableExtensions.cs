namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// <see cref=“irunnable”/>的一些扩展方法。
    /// </summary>
    public static class RunnableExtensions
    {
        /// <summary>
        /// 回调 <see cref="IRunnable.Stop"/> 然后<see cref="IRunnable.WaitToStop"/>.
        /// </summary>
        public static void StopAndWaitToStop(this IRunnable runnable)
        {
            runnable.Stop();
            runnable.WaitToStop();
        }
    }
}