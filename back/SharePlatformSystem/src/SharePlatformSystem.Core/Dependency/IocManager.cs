using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Proxy;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 此类用于直接执行依赖项注入任务。
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// 单例实例。
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        ///castle proxygenerator的单音实例。
        ///从castle.core文档中，强烈建议使用proxygenerator的单个实例以避免内存泄漏和性能问题 
        /// </summary>
        private static readonly ProxyGenerator ProxyGeneratorInstance = new ProxyGenerator();

        /// <summary>
        ///引用Castle Windsor容器.
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// 所有已注册常规注册人的名单。
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// 创建新的“iocmanager”对象。
        ///通常，您不会直接实例化“iocmanager”。
        ///这可能对测试有用。
        /// </summary>
        public IocManager()
        {
            IocContainer = CreateContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //Register self!
            IocContainer.Register(
                Component
                    .For<IocManager, IIocManager, IIocRegistrar, IIocResolver>()
                    .Instance(this)
            );
        }

        protected virtual IWindsorContainer CreateContainer()
        {
            return new WindsorContainer(new DefaultProxyFactory(ProxyGeneratorInstance));
        }

        /// <summary>
        ///为常规注册添加依赖项注册器。
        /// </summary>
        /// <param name="registrar">依赖关系注册器</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// 由所有常规注册器注册给定程序集的类型。参见“addConventionalRegistrar”方法。
        /// </summary>
        /// <param name="assembly">要注册的程序集</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// 由所有常规注册器注册给定程序集的类型。参见“addConventionalRegistrar”方法。
        /// </summary>
        /// <param name="assembly">要注册的程序集</param>
        /// <param name="config">附加配置</param>
        public void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config)
        {
            var context = new ConventionalRegistrationContext(assembly, this, config);

            foreach (var registerer in _conventionalRegistrars)
            {
                registerer.RegisterAssembly(context);
            }

            if (config.InstallInstallers)
            {
                IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }

        /// <summary>
        /// 将类型注册为自注册。
        /// </summary>
        /// <typeparam name="TType">类的类型</typeparam>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// 将类型注册为自注册。
        /// </summary>
        /// <param name="type">类的类型</param>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        ///用它的实现注册一个类型。
        /// </summary>
        /// <typeparam name="TType">正在注册类型</typeparam>
        /// <typeparam name="TImpl">实现“ttype”的类型</typeparam>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        /// <summary>
        /// 用它的实现注册一个类型。
        /// </summary>
        /// <param name="type">类的类型</param>
        /// <param name="impl">实现“类型”的类型</param>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        ///检查给定类型之前是否已注册。
        /// </summary>
        /// <param name="type">类型检查</param>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <typeparam name="TType">类型检查</typeparam>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        ///从IOC容器获取对象。
        ///返回的对象必须在使用后释放（请参见“iiocresolver.release”）。
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <returns>实例对象</returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        ///从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <typeparam name="T">要强制转换的对象的类型</typeparam>
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        /// <summary>
        /// 从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“IIocResolver.Release”）。
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>实例对象</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        ///从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“IIocResolver.Release”）。
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <returns>实例对象</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// 从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“IIocResolver.Release”）。
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>实例对象</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll<T>(argumentsAsAnonymousType);
        }

        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll(type, argumentsAsAnonymousType).Cast<object>().ToArray();
        }

        /// <summary>
        /// 释放预先解析的对象。请参见解决方法。
        /// </summary>
        /// <param name="obj">要释放的对象</param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        public void Dispose()
        {
            IocContainer.Dispose();
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}