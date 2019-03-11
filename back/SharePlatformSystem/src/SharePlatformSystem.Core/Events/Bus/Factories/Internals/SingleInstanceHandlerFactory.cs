using System;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories.Internals
{
    /// <summary>
    /// ��ʵ�����ڴ����¼�
    ///ͨ������ʵ������
    /// </summary>
    /// <remarks>
    /// ����ʼ�ջ�ȡͬһ���������ʵ����
    /// </remarks>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        ///�¼��������ʵ����
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