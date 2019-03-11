using System;
using System.Threading.Tasks;
using SharePlatformSystem.Events.Bus.Factories;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    /// 定义事件总线的接口。
    /// </summary>
    public interface IEventBus
    {
        #region Register

        /// <summary>
        ///注册到事件。
        ///对所有事件事件调用给定的操作。
        /// </summary>
        /// <param name="action">处理事件的操作</param>
        /// <typeparam name="TEventData">事件类型</typeparam>
        IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        ///注册到事件。
        ///对所有事件事件调用给定的操作。
        /// </summary>
        /// <param name="action">处理事件的操作</param>
        /// <typeparam name="TEventData">事件类型</typeparam>
        IDisposable AsyncRegister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData;

        /// <summary>
        ///注册到事件。
        ///处理程序的相同（给定）实例用于所有事件发生。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">处理事件的对象</param>
        IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        ///注册到事件。
        ///异步处理程序的相同（给定）实例用于所有事件发生。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">处理事件的对象</param>
        IDisposable AsyncRegister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        ///注册到事件。
        ///为每次事件发生创建一个新的<see cref=“thandler”/>对象实例。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">处理事件的对象</param>
        IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler, new();

        /// <summary>
        ///注册到事件。
        ///处理程序的相同（给定）实例用于所有事件发生
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">处理事件的对象</param>
        IDisposable Register(Type eventType, IEventHandler handler);

        /// <summary>
        ///注册到事件。
        ///给定的工厂用于创建/释放处理程序
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="factory">创建/发布处理程序的工厂</param>
        IDisposable Register<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event.
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">创建/发布处理程序的工厂</param>
        IDisposable Register(Type eventType, IEventHandlerFactory factory);

        #endregion

        #region Unregister

        /// <summary>
        /// 从事件中注销。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="action"></param>
        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        ///从事件中注销。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="action"></param>
        void AsyncUnregister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData;

        /// <summary>
        /// 从事件中注销。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">以前注册的处理程序对象</param>
        void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// 从事件中注销。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">以前注册的处理程序对象</param>
        void AsyncUnregister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// 从事件中注销。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="handler">以前注册的处理程序对象</param>
        void Unregister(Type eventType, IEventHandler handler);

        /// <summary>
        /// 从事件中注销。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="factory">以前注册的工厂对象</param>
        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// 从事件中注销。
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">以前注册的工厂对象</param>
        void Unregister(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// 注销给定事件类型的所有事件处理程序。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        void UnregisterAll<TEventData>() where TEventData : IEventData;

        /// <summary>
        /// 注销给定事件类型的所有事件处理程序。
        /// </summary>
        /// <param name="eventType">事件类型</param>
        void UnregisterAll(Type eventType);

        #endregion

        #region Trigger

        /// <summary>
        ///触发事件。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventData">事件的相关数据</param>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 触发事件。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventSource">触发事件的对象</param>
        /// <param name="eventData">事件的相关数据</param>
        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        ///触发事件。
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventData">事件的相关数据</param>
        void Trigger(Type eventType, IEventData eventData);

        /// <summary>
        ///触发事件。
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventSource">触发事件的对象</param>
        /// <param name="eventData">事件的相关数据</param>
        void Trigger(Type eventType, object eventSource, IEventData eventData);

        /// <summary>
        /// 异步触发事件。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventData">事件的相关数据</param>
        /// <returns>处理异步操作的任务</returns>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 异步触发事件。
        /// </summary>
        /// <typeparam name="TEventData">事件类型</typeparam>
        /// <param name="eventSource">触发事件的对象</param>
        /// <param name="eventData">事件的相关数据</param>
        /// <returns>处理异步操作的任务</returns>
        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 异步触发事件。
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventData">事件的相关数据</param>
        /// <returns>处理异步操作的任务</returns>
        Task TriggerAsync(Type eventType, IEventData eventData);

        /// <summary>
        ///异步触发事件。
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventSource">触发事件的对象</param>
        /// <param name="eventData">事件的相关数据</param>
        /// <returns>处理异步操作的任务</returns>
        Task TriggerAsync(Type eventType, object eventSource, IEventData eventData);


        #endregion
    }
}