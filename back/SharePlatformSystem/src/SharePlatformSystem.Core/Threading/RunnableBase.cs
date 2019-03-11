namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// <see cref=“irunnable”/>的基本实现。
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        /// <summary>
        /// 用于控制运行的布尔值。
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        private volatile bool _isRunning;

        public virtual void Start()
        {
            _isRunning = true;
        }

        public virtual void Stop()
        {
            _isRunning = false;
        }

        public virtual void WaitToStop()
        {

        }
    }
}