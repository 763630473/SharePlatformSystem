using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;
using System;
using System.Threading;
namespace SharePlatformSystem.Threading.Timers
{
    /// <summary>
    /// 一种Roboust定时器实现，确保不会发生重叠。它在节拍之间精确地等待指定的时间。
    /// </summary>
    //TODO: 提取接口或使所有成员成为虚拟的，以便于测试。
    public class SharePlatformTimer : RunnableBase, ITransientDependency
    {
        /// <summary>
        /// 此事件根据计时器的周期定期引发。
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// 计时器的任务周期（毫秒）。
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// 指示计时器是否在计时器的Start方法上引发一次Elapsed事件。
        /// Default: False.
        /// </summary>
        public bool RunOnStart { get; set; }

        /// <summary>
        /// 此计时器用于以指定的间隔执行任务。
        /// </summary>
        private readonly Timer _taskTimer;

        /// <summary>
        /// 指示计时器是运行还是停止。
        /// </summary>
        private volatile bool _running;

        /// <summary>
        ///指示执行任务还是执行任务计时器处于睡眠模式。
        ///此字段用于在停止计时器时等待执行任务。
        /// </summary>
        private volatile bool _performingTasks;

        /// <summary>
        /// Creates a new Timer.
        /// </summary>
        public SharePlatformTimer()
        {
            _taskTimer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        ///创建新计时器。
        /// </summary>
        /// <param name="period">计时器的任务周期（毫秒）</param>
        /// <param name="runOnStart">指示计时器是否在计时器的Start方法上引发一次经过的事件</param>
        public SharePlatformTimer(int period, bool runOnStart = false)
            : this()
        {
            Period = period;
            RunOnStart = runOnStart;
        }

        /// <summary>
        ///启动计时器。
        /// </summary>
        public override void Start()
        {
            if (Period <= 0)
            {
                throw new SharePlatformException("应在启动计时器之前设置周期！");
            }

            base.Start();

            _running = true;
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
        }

        /// <summary>
        ///停止计时器。
        /// </summary>
        public override void Stop()
        {
            lock (_taskTimer)
            {
                _running = false;
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            base.Stop();
        }

        /// <summary>
        /// 等待服务停止。
        /// </summary>
        public override void WaitToStop()
        {
            lock (_taskTimer)
            {
                while (_performingTasks)
                {
                    Monitor.Wait(_taskTimer);
                }
            }

            base.WaitToStop();
        }

        /// <summary>
        /// 此方法由任务计时器调用。
        /// </summary>
        /// <param name="state">未使用的参数</param>
        private void TimerCallBack(object state)
        {
            lock (_taskTimer)
            {
                if (!_running || _performingTasks)
                {
                    return;
                }

                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }

            try
            {
                if (Elapsed != null)
                {
                    Elapsed(this, new EventArgs());
                }
            }
            catch
            {

            }
            finally
            {
                lock (_taskTimer)
                {
                    _performingTasks = false;
                    if (_running)
                    {
                        _taskTimer.Change(Period, Timeout.Infinite);
                    }

                    Monitor.Pulse(_taskTimer);
                }
            }
        }
    }
}