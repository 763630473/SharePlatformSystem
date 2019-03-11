using SharePlatformSystem.Threading.Timers;
using System;

namespace SharePlatformSystem.Threading.BackgroundWorkers
{
    /// <summary>
    ///��չ<see cref=��backgroundWorkerBase��/>����Ӷ������еļ�ʱ����
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        protected readonly SharePlatformTimer Timer;

        /// <summary>
        /// ��ʼ�������ʵ����
        /// </summary>
        /// <param name="timer">��ʱ����</param>
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
        /// �����ʱ���������¼���
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
        /// Ӧͨ��ʵʩ�÷�������ɶ��ڹ�����
        /// </summary>
        protected abstract void DoWork();
    }
}