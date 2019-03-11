using System.Threading.Tasks;

namespace SharePlatformSystem.Events.Bus.Handlers
{
    /// <summary>
    /// 定义一个类的接口，该类异步处理类型为的事件。
    /// </summary>
    /// <typeparam name="TEventData">要处理的事件类型</typeparam>
    public interface IAsyncEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        ///处理程序通过实现此方法来处理事件。
        /// </summary>
        /// <param name="eventData">Event data</param>
        Task HandleEventAsync(TEventData eventData);
    }
}
