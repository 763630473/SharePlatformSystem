using System;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories.Internals
{
    /// <summary>
    ///�ˡ�ieventhandlerFactory��ʵ�����ڴ����¼�
    ///��ʱʵ������
    /// </summary>
    /// <remarks>
    /// ����ʼ�մ���������������ʱʵ����
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory 
        where THandler : IEventHandler, new()
    {
        /// <summary>
        ///�����������������ʵ����
        /// </summary>
        /// <returns>����������</returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        public Type GetHandlerType()
        {
            return typeof(THandler);
        }

        /// <summary>
        /// ���handler����<see cref=��idisposable��/>�����ͷ���������ʲôҲ������
        /// </summary>
        /// <param name="handler">Ҫ�ͷŵĴ������</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}