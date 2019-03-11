using SharePlatformSystem.Threading.Timers;
using System;

namespace SharePlatformSystem.Threading.BackgroundWorkers
{
    /// <summary>
    ///扩展<see cref=“backgroundWorkerBase”/>以添加定期运行的计时器。
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        protected readonly SharePlatformTimer Timer;

        /// <summary>
        /// 初始化类的新实例。
        /// </summary>
        /// <param name="timer">计时器。</param>
        protected PeriodicBackgroundWorkerBase(SharePlatformTimer timer)
        {
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }

        public override void Start()
        {
            base.Start();
            Timer.Start();
        }

        public override void Stop()
        {
            Timer.Stop();
            base.Stop();
        }

        public override void WaitToStop()
        {
            Timer.WaitToStop();
            base.WaitToStop();
        }

        /// <summary>
        /// 处理计时器的已用事件。
        /// </summary>
        private void Timer_Elapsed(object sender, System.EventArgs e)
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        /// <summary>
        /// 应通过实施该方法来完成定期工作。
        /// </summary>
        protected abstract void DoWork();
    }
}