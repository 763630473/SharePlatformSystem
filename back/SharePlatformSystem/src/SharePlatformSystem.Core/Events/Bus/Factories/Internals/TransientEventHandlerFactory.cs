using System;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories.Internals
{
    /// <summary>
    ///此“ieventhandlerFactory”实现用于处理事件
    ///临时实例对象
    /// </summary>
    /// <remarks>
    /// 此类始终创建处理程序的新临时实例。
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory 
        where THandler : IEventHandler, new()
    {
        /// <summary>
        ///创建处理程序对象的新实例。
        /// </summary>
        /// <returns>处理程序对象</returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        public Type GetHandlerType()
        {
            return typeof(THandler);
        }

        /// <summary>
        /// 如果handler对象<see cref=“idisposable”/>，则释放它。否则什么也不做。
        /// </summary>
        /// <param name="handler">要释放的处理程序</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}