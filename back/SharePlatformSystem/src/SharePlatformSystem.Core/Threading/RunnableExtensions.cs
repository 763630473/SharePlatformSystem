namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// <see cref=��irunnable��/>��һЩ��չ������
    /// </summary>
    public static class RunnableExtensions
    {
        /// <summary>
        /// �ص� <see cref="IRunnable.Stop"/> Ȼ��<see cref="IRunnable.WaitToStop"/>.
        /// </summary>
        public static void StopAndWaitToStop(this IRunnable runnable)
        {
            runnable.Stop();
            runnable.WaitToStop();
        }
    }
}