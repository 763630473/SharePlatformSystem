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
    /// ��������ֱ��ִ��������ע������
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// ����ʵ����
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        ///castle proxygenerator�ĵ���ʵ����
        ///��castle.core�ĵ��У�ǿ�ҽ���ʹ��proxygenerator�ĵ���ʵ���Ա����ڴ�й©���������� 
        /// </summary>
        private static readonly ProxyGenerator ProxyGeneratorInstance = new ProxyGenerator();

        /// <summary>
        ///����Castle Windsor����.
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// ������ע�᳣��ע���˵�������
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// �����µġ�iocmanager������
        ///ͨ����������ֱ��ʵ������iocmanager����
        ///����ܶԲ������á�
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
        ///Ϊ����ע�����������ע������
        /// </summary>
        /// <param name="registrar">������ϵע����</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// �����г���ע����ע��������򼯵����͡��μ���addConventionalRegistrar��������
        /// </summary>
        /// <param name="assembly">Ҫע��ĳ���</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// �����г���ע����ע��������򼯵����͡��μ���addConventionalRegistrar��������
        /// </summary>
        /// <param name="assembly">Ҫע��ĳ���</param>
        /// <param name="config">��������</param>
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
        /// ������ע��Ϊ��ע�ᡣ
        /// </summary>
        /// <typeparam name="TType">�������</typeparam>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// ������ע��Ϊ��ע�ᡣ
        /// </summary>
        /// <param name="type">�������</param>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        ///������ʵ��ע��һ�����͡�
        /// </summary>
        /// <typeparam name="TType">����ע������</typeparam>
        /// <typeparam name="TImpl">ʵ�֡�ttype��������</typeparam>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        /// <summary>
        /// ������ʵ��ע��һ�����͡�
        /// </summary>
        /// <param name="type">�������</param>
        /// <param name="impl">ʵ�֡����͡�������</param>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        ///����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <param name="type">���ͼ��</param>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <typeparam name="TType">���ͼ��</typeparam>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        ///��IOC������ȡ����
        ///���صĶ��������ʹ�ú��ͷţ���μ���iiocresolver.release������
        /// </summary> 
        /// <typeparam name="T">Ҫ��ȡ�Ķ��������</typeparam>
        /// <returns>ʵ������</returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        ///��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <typeparam name="T">Ҫǿ��ת���Ķ��������</typeparam>
        /// <param name="type">Ҫ�����Ķ��������</param>
        /// <returns>����ʵ��</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        /// <summary>
        /// ��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���IIocResolver.Release������
        /// </summary> 
        /// <typeparam name="T">Ҫ��ȡ�Ķ��������</typeparam>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns>ʵ������</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        ///��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���IIocResolver.Release������
        /// </summary> 
        /// <param name="type">Ҫ��ȡ�Ķ��������</param>
        /// <returns>ʵ������</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// ��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���IIocResolver.Release������
        /// </summary> 
        /// <param name="type">Ҫ��ȡ�Ķ��������</param>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns>ʵ������</returns>
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
        /// �ͷ�Ԥ�Ƚ����Ķ�����μ����������
        /// </summary>
        /// <param name="obj">Ҫ�ͷŵĶ���</param>
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