using System;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// ���¼����������һ�����������ܹ�ʹ�ò�����Ϊʵ�֡�
    /// </summary>
    /// <typeparam name="TEventData">Event type</typeparam>
    internal class ActionEventHandler<TEventData> :
        IEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// �����¼��Ĳ�����
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// ����<see cref=��actionEventHandler teventData��/>����ʵ����
        /// </summary>
        /// <param name="handler">Action to handle the event</param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// �����¼���
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}