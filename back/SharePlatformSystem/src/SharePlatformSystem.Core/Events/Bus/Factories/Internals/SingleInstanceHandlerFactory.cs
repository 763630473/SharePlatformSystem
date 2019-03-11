using System;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories.Internals
{
    /// <summary>
    /// 此实现用于处理事件
    ///通过单个实例对象。
    /// </summary>
    /// <remarks>
    /// 此类始终获取同一个处理程序实例。
    /// </remarks>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        ///事件处理程序实例。
        /// </summary>
        public IEventHandler HandlerInstance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        public IEventHandler GetHandler()
        {
            return HandlerInstance;
        }

        public Type GetHandlerType()
        {
            return ProxyHelper.UnProxy(HandlerInstance).GetType();
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            
        }
    }
}