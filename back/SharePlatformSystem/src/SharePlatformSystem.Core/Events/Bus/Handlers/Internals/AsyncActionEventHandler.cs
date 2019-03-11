using System;
using System.Threading.Tasks;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// 此事件处理程序是一个适配器，能够使用操作作为实现。
    /// </summary>
    /// <typeparam name="TEventData">Event type</typeparam>
    internal class AsyncActionEventHandler<TEventData> :
        IAsyncEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// 函数来处理事件。
        /// </summary>
        public Func<TEventData, Task> Action { get; private set; }

        /// <summary>
        /// 创建<see cref=“asyncActionEventHandler teventData”/>的新实例。
        /// </summary>
        /// <param name="handler">处理事件的操作</param>
        public AsyncActionEventHandler(Func<TEventData, Task> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// 处理事件。
        /// </summary>
        /// <param name="eventData"></param>
        public async Task HandleEventAsync(TEventData eventData)
        {
            await Action(eventData);
        }
    }
}