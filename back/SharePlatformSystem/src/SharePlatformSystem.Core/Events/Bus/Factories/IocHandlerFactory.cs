using System;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories
{
    /// <summary>
    /// ��ʵ�����ڻ�ȡ/����
    ///ʹ��IOC�Ĵ������
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        ///�����������͡�
        /// </summary>
        public Type HandlerType { get; }

        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// ���������ʵ����
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="handlerType">Type of the handler</param>
        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        /// <summary>
        /// ��IOC������������������
        /// </summary>
        /// <returns>�ѽ�������������</returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        public Type GetHandlerType()
        {
            return HandlerType;
        }

        /// <summary>
        /// ʹ��IOC�����ͷŴ���������
        /// </summary>
        /// <param name="handler">Ҫ�ͷŵĴ������</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}