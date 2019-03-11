using System;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    /// ʵ�ֲ�Ϊ�¼��������ṩ������
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        ///�¼�������ʱ�䡣
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// �����¼��Ķ��󣨿�ѡ����
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// ���캯��.
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }
    }
}