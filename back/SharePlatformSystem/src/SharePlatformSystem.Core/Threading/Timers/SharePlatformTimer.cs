using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;
using System;
using System.Threading;
namespace SharePlatformSystem.Threading.Timers
{
    /// <summary>
    /// һ��Roboust��ʱ��ʵ�֣�ȷ�����ᷢ���ص������ڽ���֮�侫ȷ�صȴ�ָ����ʱ�䡣
    /// </summary>
    //TODO: ��ȡ�ӿڻ�ʹ���г�Ա��Ϊ����ģ��Ա��ڲ��ԡ�
    public class SharePlatformTimer : RunnableBase, ITransientDependency
    {
        /// <summary>
        /// ���¼����ݼ�ʱ�������ڶ���������
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// ��ʱ�����������ڣ����룩��
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// ָʾ��ʱ���Ƿ��ڼ�ʱ����Start����������һ��Elapsed�¼���
        /// Default: False.
        /// </summary>
        public bool RunOnStart { get; set; }

        /// <summary>
        /// �˼�ʱ��������ָ���ļ��ִ������
        /// </summary>
        private readonly Timer _taskTimer;

        /// <summary>
        /// ָʾ��ʱ�������л���ֹͣ��
        /// </summary>
        private volatile bool _running;

        /// <summary>
        ///ָʾִ��������ִ�������ʱ������˯��ģʽ��
        ///���ֶ�������ֹͣ��ʱ��ʱ�ȴ�ִ������
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
        ///�����¼�ʱ����
        /// </summary>
        /// <param name="period">��ʱ�����������ڣ����룩</param>
        /// <param name="runOnStart">ָʾ��ʱ���Ƿ��ڼ�ʱ����Start����������һ�ξ������¼�</param>
        public SharePlatformTimer(int period, bool runOnStart = false)
            : this()
        {
            Period = period;
            RunOnStart = runOnStart;
        }

        /// <summary>
        ///������ʱ����
        /// </summary>
        public override void Start()
        {
            if (Period <= 0)
            {
                throw new SharePlatformException("Ӧ��������ʱ��֮ǰ�������ڣ�");
            }

            base.Start();

            _running = true;
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
        }

        /// <summary>
        ///ֹͣ��ʱ����
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
        /// �ȴ�����ֹͣ��
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
        /// �˷����������ʱ�����á�
        /// </summary>
        /// <param name="state">δʹ�õĲ���</param>
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