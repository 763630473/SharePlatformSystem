using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 将方法扩展到“IIocredolver”接口.
    /// </summary>
    public static class IocResolverExtensions
    {
        /// <summary>
        ///获取将解析对象包装为可释放的“DisposableDependencyObjectWrapper”t对象。
        /// </summary> 
        /// <typeparam name="T">获取的对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <returns>由“DisposableDependencyObjectWrapper”包装的实例对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>());
        }

        /// <summary>
        ///获取将解析对象包装为可释放的“DisposableDependencyObjectWrapper”t对象。
        /// </summary> 
        /// <typeparam name="T">获取的对象的类型</typeparam>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible <typeparamref name="T"/>.</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type));
        }

        /// <summary>
        ///获取将解析对象包装为可释放的“DisposableDependencyObjectWrapper”t对象。
        /// </summary> 
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible to <see cref="IDisposable"/>.</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type));
        }

        /// <summary>
        ///获取将解析对象包装为可释放的“DisposableDependencyObjectWrapper”t对象。
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>(argumentsAsAnonymousType));
        }

        /// <summary>
        ///获取将解析对象包装为可释放的“DisposableDependencyObjectWrapper”t对象。
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible <typeparamref name="T"/>.</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type, argumentsAsAnonymousType)); 
        }

        /// <summary>
        ///获取将解析对象包装为可释放的“DisposableDependencyObjectWrapper”t对象。
        /// </summary> 
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible to <see cref="IDisposable"/>.</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The instance object wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type, argumentsAsAnonymousType));
        }

        /// <summary>
        ///获取将解析对象包装为可释放的“ScopedIocResolver”t对象。
        /// </summary>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <returns>The instance object wrapped by <see cref="ScopedIocResolver"/></returns>
        public static IScopedIocResolver CreateScope(this IIocResolver iocResolver)
        {
            return new ScopedIocResolver(iocResolver);
        }

        /// <summary>
        /// This method can be used to resolve and release an object automatically.
        /// You can use the object in <paramref name="action"/>.
        /// </summary> 
        /// <typeparam name="T">Type of the object to use</typeparam>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="action">An action that can use the resolved object</param>
        public static void Using<T>(this IIocResolver iocResolver, Action<T> action)
        {
            using (var wrapper = iocResolver.ResolveAsDisposable<T>())
            {
                action(wrapper.Object);
            }
        }

        /// <summary>
        /// This method can be used to resolve and release an object automatically.
        /// You can use the object in <paramref name="func"/> and return a value.
        /// </summary> 
        /// <typeparam name="TService">Type of the service to use</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="func">A function that can use the resolved object</param>
        public static TReturn Using<TService, TReturn>(this IIocResolver iocResolver, Func<TService, TReturn> func)
        {
            using (var obj = iocResolver.ResolveAsDisposable<TService>())
            {
                return func(obj.Object);
            }
        }

        /// <summary>
        /// This method starts a scope to resolve and release all objects automatically.
        /// You can use the <c>scope</c> in <see cref="action"/>.
        /// </summary> 
        /// <param name="iocResolver">IIocResolver对象</param>
        /// <param name="action">An action that can use the resolved object</param>
        public static void UsingScope(this IIocResolver iocResolver, Action<IScopedIocResolver> action)
        {
            using (var scope = iocResolver.CreateScope())
            {
                action(scope);
            }
        }
    }
}
