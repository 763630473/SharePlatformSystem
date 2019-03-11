using System;
using System.Threading.Tasks;
using SharePlatformSystem.Events.Bus.Factories;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    /// �����¼����ߵĽӿڡ�
    /// </summary>
    public interface IEventBus
    {
        #region Register

        /// <summary>
        ///ע�ᵽ�¼���
        ///�������¼��¼����ø����Ĳ�����
        /// </summary>
        /// <param name="action">�����¼��Ĳ���</param>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        ///ע�ᵽ�¼���
        ///�������¼��¼����ø����Ĳ�����
        /// </summary>
        /// <param name="action">�����¼��Ĳ���</param>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        IDisposable AsyncRegister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData;

        /// <summary>
        ///ע�ᵽ�¼���
        ///����������ͬ��������ʵ�����������¼�������
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">�����¼��Ķ���</param>
        IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        ///ע�ᵽ�¼���
        ///�첽����������ͬ��������ʵ�����������¼�������
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">�����¼��Ķ���</param>
        IDisposable AsyncRegister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        ///ע�ᵽ�¼���
        ///Ϊÿ���¼���������һ���µ�<see cref=��thandler��/>����ʵ����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">�����¼��Ķ���</param>
        IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler, new();

        /// <summary>
        ///ע�ᵽ�¼���
        ///����������ͬ��������ʵ�����������¼�����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">�����¼��Ķ���</param>
        IDisposable Register(Type eventType, IEventHandler handler);

        /// <summary>
        ///ע�ᵽ�¼���
        ///�����Ĺ������ڴ���/�ͷŴ������
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="factory">����/�����������Ĺ���</param>
        IDisposable Register<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event.
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="factory">����/�����������Ĺ���</param>
        IDisposable Register(Type eventType, IEventHandlerFactory factory);

        #endregion

        #region Unregister

        /// <summary>
        /// ���¼���ע����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="action"></param>
        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        ///���¼���ע����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="action"></param>
        void AsyncUnregister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData;

        /// <summary>
        /// ���¼���ע����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">��ǰע��Ĵ���������</param>
        void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// ���¼���ע����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">��ǰע��Ĵ���������</param>
        void AsyncUnregister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// ���¼���ע����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">��ǰע��Ĵ���������</param>
        void Unregister(Type eventType, IEventHandler handler);

        /// <summary>
        /// ���¼���ע����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="factory">��ǰע��Ĺ�������</param>
        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// ���¼���ע����
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="factory">��ǰע��Ĺ�������</param>
        void Unregister(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// ע�������¼����͵������¼��������
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        void UnregisterAll<TEventData>() where TEventData : IEventData;

        /// <summary>
        /// ע�������¼����͵������¼��������
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        void UnregisterAll(Type eventType);

        #endregion

        #region Trigger

        /// <summary>
        ///�����¼���
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventData">�¼����������</param>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// �����¼���
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventSource">�����¼��Ķ���</param>
        /// <param name="eventData">�¼����������</param>
        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        ///�����¼���
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventData">�¼����������</param>
        void Trigger(Type eventType, IEventData eventData);

        /// <summary>
        ///�����¼���
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventSource">�����¼��Ķ���</param>
        /// <param name="eventData">�¼����������</param>
        void Trigger(Type eventType, object eventSource, IEventData eventData);

        /// <summary>
        /// �첽�����¼���
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventData">�¼����������</param>
        /// <returns>�����첽����������</returns>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// �첽�����¼���
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventSource">�����¼��Ķ���</param>
        /// <param name="eventData">�¼����������</param>
        /// <returns>�����첽����������</returns>
        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// �첽�����¼���
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventData">�¼����������</param>
        /// <returns>�����첽����������</returns>
        Task TriggerAsync(Type eventType, IEventData eventData);

        /// <summary>
        ///�첽�����¼���
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventSource">�����¼��Ķ���</param>
        /// <param name="eventData">�¼����������</param>
        /// <returns>�����첽����������</returns>
        Task TriggerAsync(Type eventType, object eventSource, IEventData eventData);


        #endregion
    }
}