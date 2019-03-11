using System;
using System.Threading.Tasks;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// ���¼����������һ�����������ܹ�ʹ�ò�����Ϊʵ�֡�
    /// </summary>
    /// <typeparam name="TEventData">Event type</typeparam>
    internal class AsyncActionEventHandler<TEventData> :
        IAsyncEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// �����������¼���
        /// </summary>
        public Func<TEventData, Task> Action { get; private set; }

        /// <summary>
        /// ����<see cref=��asyncActionEventHandler teventData��/>����ʵ����
        /// </summary>
        /// <param name="handler">�����¼��Ĳ���</param>
        public AsyncActionEventHandler(Func<TEventData, Task> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// �����¼���
        /// </summary>
        /// <param name="eventData"></param>
        public async Task HandleEventAsync(TEventData eventData)
        {
            await Action(eventData);
        }
    }
}