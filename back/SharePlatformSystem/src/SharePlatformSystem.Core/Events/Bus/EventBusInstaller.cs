using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Events.Bus.Factories;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    ///安装事件总线系统并自动注册所有处理程序。
    /// </summary>
    internal class EventBusInstaller : IWindsorInstaller
    {
        private readonly IIocResolver _iocResolver;
        private readonly IEventBusConfiguration _eventBusConfiguration;
        private IEventBus _eventBus;

        public EventBusInstaller(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _eventBusConfiguration = iocResolver.Resolve<IEventBusConfiguration>();
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_eventBusConfiguration.UseDefaultEventBus)
            {
                container.Register(
                    Component.For<IEventBus>().Instance(EventBus.Default).LifestyleSingleton()
                );
            }
            else
            {
                container.Register(
                    Component.For<IEventBus>().ImplementedBy<EventBus>().LifestyleSingleton()
                    );
            }

            _eventBus = container.Resolve<IEventBus>();

            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            /* 此代码检查注册组件是否实现任何ieventhandler<teventdata>接口，如果是，
             *获取每个处理事件的所有事件处理程序接口并将类型注册到事件总线。
             */
            if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                return;
            }

            var interfaces = handler.ComponentModel.Implementation.GetTypeInfo().GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    _eventBus.Register(genericArgs[0], new IocHandlerFactory(_iocResolver, handler.ComponentModel.Implementation));
                }
            }
        }
    }
}
