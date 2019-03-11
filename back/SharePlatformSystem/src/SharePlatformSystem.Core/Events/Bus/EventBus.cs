using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Events.Bus.Factories;
using SharePlatformSystem.Events.Bus.Factories.Internals;
using SharePlatformSystem.Events.Bus.Handlers;
using SharePlatformSystem.Events.Bus.Handlers.Internals;
using SharePlatformSystem.Threading;
using SharePlatformSystem.Threading.Extensions;

namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    /// ��EventBusʵ��Ϊ����ģʽ��
    /// </summary>
    public class EventBus : IEventBus
    {
        /// <summary>
        /// ��ȡĬ�ϵ�<see cref=��eventbus��/>ʵ����
        /// </summary>
        public static EventBus Default { get; } = new EventBus();

        /// <summary>
        /// �Լ�¼�������á�
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///����ע��Ĵ�����򹤳���
        ///�����¼�����
        ///ֵ��������򹤳��б�
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<IEventHandlerFactory>> _handlerFactories;

        /// <summary>
        ///����һ���µ�<see cref=��eventbus��/>ʵ����
        ///������ʹ��<see cref=��default��/>��ʹ��global<see cref=��eventbus��/>�������Ǵ����µ�ʵ����
        /// </summary>
        public EventBus()
        {
            _handlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
            Logger = NullLogger.Instance;
        }

        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return Register(typeof(TEventData), new ActionEventHandler<TEventData>(action));
        }

        public IDisposable AsyncRegister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData
        {
            return Register(typeof(TEventData), new AsyncActionEventHandler<TEventData>(action));
        }

        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handler);
        }

        public IDisposable AsyncRegister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handler);
        }

        public IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData
            where THandler : IEventHandler, new()
        {
            return Register(typeof(TEventData), new TransientEventHandlerFactory<THandler>());
        }

        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            return Register(eventType, new SingleInstanceHandlerFactory(handler));
        }

        public IDisposable Register<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            return Register(typeof(TEventData), factory);
        }

        public IDisposable Register(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories => factories.Add(factory));

            return new FactoryUnregistrar(this, eventType, factory);
        }

        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            Check.NotNull(action, nameof(action));

            GetOrCreateHandlerFactories(typeof(TEventData))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEventData>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
                });
        }

        public void AsyncUnregister<TEventData>(Func<TEventData, Task> action) where TEventData : IEventData
        {
            Check.NotNull(action, nameof(action));

            GetOrCreateHandlerFactories(typeof(TEventData))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.HandlerInstance as AsyncActionEventHandler<TEventData>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
                });
        }

        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), handler);
        }

        public void AsyncUnregister<TEventData>(IAsyncEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), handler);
        }

        public void Unregister(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                            factory is SingleInstanceHandlerFactory &&
                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                        );
                });
        }

        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), factory);
        }

        public void Unregister(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
        }

        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
            UnregisterAll(typeof(TEventData));
        }

        public void UnregisterAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
        }

        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            Trigger((object)null, eventData);
        }

        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            Trigger(typeof(TEventData), eventSource, eventData);
        }

        public void Trigger(Type eventType, IEventData eventData)
        {
            Trigger(eventType, null, eventData);
        }

        public void Trigger(Type eventType, object eventSource, IEventData eventData)
        {
            var exceptions = new List<Exception>();

            eventData.EventSource = eventSource;

            foreach (var handlerFactories in GetHandlerFactories(eventType))
            {
                foreach (var handlerFactory in handlerFactories.EventHandlerFactories)
                {
                    var handlerType = handlerFactory.GetHandlerType();

                    if (IsAsyncEventHandler(handlerType))
                    {
                        AsyncHelper.RunSync(() => TriggerAsyncHandlingException(handlerFactory, handlerFactories.EventType, eventData, exceptions));
                    }
                    else if (IsEventHandler(handlerType))
                    {
                        TriggerHandlingException(handlerFactory, handlerFactories.EventType, eventData, exceptions);
                    }
                    else
                    {
                        var message = $"Event handler to register for event type {eventType.Name} does not implement IEventHandler<{eventType.Name}> or IAsyncEventHandler<{eventType.Name}> interface!";
                        exceptions.Add(new SharePlatformException(message));
                    }
                }
            }

            //ʵ�ַ��Ͳ����̳С���μ�IEventDataWithInheritableGenericArgument
            if (eventType.GetTypeInfo().IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.GetTypeInfo().BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    var baseEventData = (IEventData)Activator.CreateInstance(baseEventType, constructorArgs);
                    baseEventData.EventTime = eventData.EventTime;
                    Trigger(baseEventType, eventData.EventSource, baseEventData);
                }
            }

            if (exceptions.Any())
            {
                if (exceptions.Count == 1)
                {
                    exceptions[0].ReThrow();
                }

                throw new AggregateException("�����¼�ʱ�����������: " + eventType, exceptions);
            }
        }

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return TriggerAsync((object)null, eventData);
        }

        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            return TriggerAsync(typeof(TEventData), eventSource, eventData);
        }

        public Task TriggerAsync(Type eventType, IEventData eventData)
        {
            return TriggerAsync(eventType, null, eventData);
        }

        public async Task TriggerAsync(Type eventType, object eventSource, IEventData eventData)
        {
            var exceptions = new List<Exception>();

            eventData.EventSource = eventSource;

            await new SynchronizationContextRemover();

            foreach (var handlerFactories in GetHandlerFactories(eventType))
            {
                foreach (var handlerFactory in handlerFactories.EventHandlerFactories)
                {
                    var handlerType = handlerFactory.GetHandlerType();

                    if (IsAsyncEventHandler(handlerType))
                    {
                        await TriggerAsyncHandlingException(handlerFactory, handlerFactories.EventType, eventData, exceptions);
                    }
                    else if (IsEventHandler(handlerType))
                    {
                        TriggerHandlingException(handlerFactory, handlerFactories.EventType, eventData, exceptions);
                    }
                    else
                    {
                        var message = $"Event handler to register for event type {eventType.Name} does not implement IEventHandler<{eventType.Name}> or IAsyncEventHandler<{eventType.Name}> interface!";
                        exceptions.Add(new SharePlatformException(message));
                    }
                }
            }

            //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            if (eventType.GetTypeInfo().IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.GetTypeInfo().BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    var baseEventData = (IEventData)Activator.CreateInstance(baseEventType, constructorArgs);
                    baseEventData.EventTime = eventData.EventTime;
                    await TriggerAsync(baseEventType, eventData.EventSource, baseEventData);
                }
            }

            if (exceptions.Any())
            {
                if (exceptions.Count == 1)
                {
                    exceptions[0].ReThrow();
                }

                throw new AggregateException("�����¼�ʱ�����������: " + eventType, exceptions);
            }
        }

        private void TriggerHandlingException(IEventHandlerFactory handlerFactory, Type eventType, IEventData eventData, List<Exception> exceptions)
        {
            var eventHandler = handlerFactory.GetHandler();
            try
            {
                if (eventHandler == null)
                {
                    throw new ArgumentNullException($"Registered event handler for event type {eventType.Name} Ϊ��!");
                }

                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

                var method = handlerType.GetMethod(
                    "HandleEvent",
                    new[] { eventType }
                );

                method.Invoke(eventHandler, new object[] { eventData });
            }
            catch (TargetInvocationException ex)
            {
                exceptions.Add(ex.InnerException);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
            finally
            {
                handlerFactory.ReleaseHandler(eventHandler);
            }
        }

        private async Task TriggerAsyncHandlingException(IEventHandlerFactory asyncHandlerFactory, Type eventType, IEventData eventData, List<Exception> exceptions)
        {
            var asyncEventHandler = asyncHandlerFactory.GetHandler();

            try
            {
                if (asyncEventHandler == null)
                {
                    throw new ArgumentNullException($"Registered event handler for event type {eventType.Name} Ϊ��!");
                }

                var asyncHandlerType = typeof(IAsyncEventHandler<>).MakeGenericType(eventType);

                var method = asyncHandlerType.GetMethod(
                    "HandleEventAsync",
                    new[] { eventType }
                );

                await (Task)method.Invoke(asyncEventHandler, new object[] { eventData });
            }
            catch (TargetInvocationException ex)
            {
                exceptions.Add(ex.InnerException);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
            finally
            {
                asyncHandlerFactory.ReleaseHandler(asyncEventHandler);
            }
        }

        private bool IsEventHandler(Type handlerType)
        {
            return handlerType.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>));
        }

        private bool IsAsyncEventHandler(Type handlerType)
        {
            return handlerType.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == typeof(IAsyncEventHandler<>));
        }

        private IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

            foreach (var handlerFactory in _handlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
            {
                handlerFactoryList.Add(new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
            }

            return handlerFactoryList.ToArray();
        }

        private static bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            //Ӧ����ͬһ����
            if (handlerType == eventType)
            {
                return true;
            }

            //ӦΪ�̳����ʹ���
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return _handlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());
        }

        private class EventTypeWithEventHandlerFactories
        {
            public Type EventType { get; }

            public List<IEventHandlerFactory> EventHandlerFactories { get; }

            public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactories;
            }
        }

        private struct SynchronizationContextRemover : INotifyCompletion
        {
            public bool IsCompleted
            {
                get { return SynchronizationContext.Current == null; }
            }

            public void OnCompleted(Action continuation)
            {
                var prevContext = SynchronizationContext.Current;
                try
                {
                    SynchronizationContext.SetSynchronizationContext(null);
                    continuation();
                }
                finally
                {
                    SynchronizationContext.SetSynchronizationContext(prevContext);
                }
            }

            public SynchronizationContextRemover GetAwaiter()
            {
                return this;
            }

            public void GetResult()
            {
            }
        }
    }
}