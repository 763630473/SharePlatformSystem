using System;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    /// 实现并为事件数据类提供基础。
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        ///事件发生的时间。
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象（可选）。
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// 构造函数.
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }
    }
}