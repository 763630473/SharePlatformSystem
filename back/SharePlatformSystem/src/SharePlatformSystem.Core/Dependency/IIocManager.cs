using System;
using Castle.Windsor;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 此接口用于直接执行依赖项注入任务。
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// 对 Castle Windsor 容器的引用。
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <param name="type">Type to check</param>
        new bool IsRegistered(Type type);

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <typeparam name="T">类型检查</typeparam>
        new bool IsRegistered<T>();
    }
}