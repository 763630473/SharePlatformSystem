using System;

namespace SharePlatformSystem.Events.Bus.Factories.Internals
{
    /// <summary>
    ///����ע��<see cref=��ieventhandlerFactory��/>on<see cref=��dispose��/>������
    /// </summary>
    internal class FactoryUnregistrar : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;

        public FactoryUnregistrar(IEventBus eventBus, Type eventType, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }

        public void Dispose()
        {
            _eventBus.Unregister(_eventType, _factory);
        }
    }
}