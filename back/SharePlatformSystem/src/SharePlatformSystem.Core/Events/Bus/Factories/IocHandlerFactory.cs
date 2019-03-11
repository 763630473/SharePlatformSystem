using System;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories
{
    /// <summary>
    /// 此实现用于获取/发布
    ///使用IOC的处理程序。
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        ///处理程序的类型。
        /// </summary>
        public Type HandlerType { get; }

        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 创建类的新实例。
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="handlerType">Type of the handler</param>
        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        /// <summary>
        /// 从IOC容器解析处理程序对象。
        /// </summary>
        /// <returns>已解析处理程序对象</returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        public Type GetHandlerType()
        {
            return HandlerType;
        }

        /// <summary>
        /// 使用IOC容器释放处理程序对象。
        /// </summary>
        /// <param name="handler">要释放的处理程序</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}