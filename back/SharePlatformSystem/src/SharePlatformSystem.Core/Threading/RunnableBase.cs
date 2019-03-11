namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// <see cref=��irunnable��/>�Ļ���ʵ�֡�
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        /// <summary>
        /// ���ڿ������еĲ���ֵ��
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