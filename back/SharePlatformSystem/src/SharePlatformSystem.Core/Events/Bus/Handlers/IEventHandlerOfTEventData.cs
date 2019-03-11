namespace SharePlatformSystem.Events.Bus.Handlers
{
    /// <summary>
    ///定义处理类型事件的类的接口<see cref=“ieventhandler teventdata”/>。
    /// </summary>
    /// <typeparam name="TEventData">Event type to handle</typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        ///处理程序通过实现此方法来处理事件。
        /// </summary>
        /// <param name="eventData">Event data</param>
        void HandleEvent(TEventData eventData);
    }
}
