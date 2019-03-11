using System;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories
{
    /// <summary>
    /// Ϊ���𴴽�/��ȡ�ͷ����¼��������Ĺ�������ӿڡ�
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        ///��ȡ�¼��������
        /// </summary>
        /// <returns>�¼��������</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// ��ȡ�����������ͣ�������ʵ������
        /// </summary>
        /// <returns></returns>
        Type GetHandlerType();

        /// <summary>
        /// �ͷ��¼��������
        /// </summary>
        /// <param name="handler">�ͷŰ���</param>
        void ReleaseHandler(IEventHandler handler);
    }
}