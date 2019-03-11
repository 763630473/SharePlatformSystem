using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 为那些用于解析依赖项的类定义接口。
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 从IOC容器获取对象。
        ///返回的对象必须在使用后释放（参见“release”）。
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <returns>对象实例</returns>
        T Resolve<T>();

        /// <summary>
        /// 从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <typeparam name="T">要强制转换的对象的类型</typeparam>
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(Type type);

        /// <summary>
        ///从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <typeparam name="T">要获取的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        ///从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type);

        /// <summary>
        ///从IOC容器获取对象。
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <param name="type">要获取的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// 获取给定类型的所有实现。
        ///返回的对象在使用后必须释放（参见“release”）。
        /// </summary> 
        /// <typeparam name="T">要解析的对象的类型</typeparam>
        /// <returns>对象实例</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// 获取给定类型的所有实现。
        ///返回的对象在使用后必须释放（参见“release”）。
        /// </summary> 
        /// <typeparam name="T">要解析的对象的类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        T[] ResolveAll<T>(object argumentsAsAnonymousType);

        /// <summary>
        ///获取给定类型的所有实现。
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <param name="type">要解析的对象的类型</param>
        /// <returns>对象实例</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        ///获取给定类型的所有实现。   
        ///返回的对象在使用后必须释放（“release”）。
        /// </summary> 
        /// <param name="type">要解析的对象的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数参数</param>
        /// <returns>对象实例</returns>
        object[] ResolveAll(Type type, object argumentsAsAnonymousType);

        /// <summary>
        ///释放预先解析的对象。请参见解决方法。
        /// </summary>
        /// <param name="obj">要释放的对象</param>
        void Release(object obj);

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <param name="type">类型检查</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// 检查给定类型之前是否已注册。
        /// </summary>
        /// <typeparam name="T">类型检查</typeparam>
        bool IsRegistered<T>();
    }
}