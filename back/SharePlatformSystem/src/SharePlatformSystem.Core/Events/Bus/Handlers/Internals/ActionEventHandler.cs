using System;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// 此事件处理程序是一个适配器，能够使用操作作为实现。
    /// </summary>
    /// <typeparam name="TEventData">Event type</typeparam>
    internal class ActionEventHandler<TEventData> :
        IEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// 处理事件的操作。
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// 创建<see cref=“actionEventHandler teventData”/>的新实例。
        /// </summary>
        /// <param name="handler">Action to handle the event</param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// 处理事件。
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}