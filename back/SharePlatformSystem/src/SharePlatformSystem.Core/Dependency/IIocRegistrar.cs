using System;
using System.Reflection;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 为那些用于注册依赖项的类定义接口。
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// 为常规注册添加依赖项注册器。
        /// </summary>
        /// <param name="registrar">依赖关系注册器</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        ///由所有常规注册器注册给定程序集的类型。请参阅“iocmanager.addConventionalRegistrar”方法。
        /// </summary>
        /// <param name="assembly">要注册的程序集</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        ///由所有常规注册器注册给定程序集的类型。请参阅“iocmanager.addConventionalRegistrar”方法。
        /// </summary>
        /// <param name="assembly">要注册的程序集</param>
        /// <param name="config">附加配置</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);

        /// <summary>
        /// 将类型注册为自注册。
        /// </summary>
        /// <typeparam name="T">类的类型</typeparam>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// 将类型注册为自注册。
        /// </summary>
        /// <param name="type">类的类型</param>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 用它的实现注册一个类型。
        /// </summary>
        /// <typeparam name="TType">正在注册类型</typeparam>
        /// <typeparam name="TImpl">实现的类型“ttype”</typeparam>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// 用它的实现注册一个类型。
        /// </summary>
        /// <param name="type">类的类型</param>
        /// <param name="impl">实现的类型"type"</param>
        /// <param name="lifeStyle">这类物品的生活方式</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <param name="type">类型检查</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <typeparam name="TType">类型检查</typeparam>
        bool IsRegistered<TType>();
    }
}