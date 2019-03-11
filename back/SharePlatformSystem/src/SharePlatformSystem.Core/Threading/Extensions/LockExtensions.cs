using System;

namespace SharePlatformSystem.Threading.Extensions
{
    /// <summary>
    /// 扩展方法使锁定更容易。
    /// </summary>
    public static class LockExtensions
    {
        /// <summary>
        ///通过锁定给定的对象来执行给定的<paramref name=“action”/>。
        /// </summary>
        /// <param name="source">源对象（要锁定）</param>
        /// <param name="action">行动（待执行）</param>
        public static void Locking(this object source, Action action)
        {
            lock (source)
            {
                action();
            }
        }

        /// <summary>
        /// 通过锁定给定的对象来执行给定的<paramref name=“action”/>。
        /// </summary>
        /// <typeparam name="T">对象类型（要锁定）</typeparam>
        /// <param name="source">源对象（要锁定）</param>
        /// <param name="action">行动（待执行）</param>
        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }

        /// <summary>
        ///执行给定的<paramref name=“func”/>，并通过锁定给定的<paramref name=“source”/>对象来返回其值。
        /// </summary>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <param name="source">源对象（要锁定）</param>
        /// <param name="func">功能（待执行）</param>
        /// <returns><paramref name=“func”/>的返回值</returns>
        public static TResult Locking<TResult>(this object source, Func<TResult> func)
        {
            lock (source)
            {
                return func();
            }
        }

        /// <summary>
        /// 执行给定的<paramref name=“func”/>，并通过锁定给定的<paramref name=“source”/>对象来返回其值。
        /// </summary>
        /// <typeparam name="T">对象类型（要锁定）</typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="source">源对象（要锁定）</param>
        /// <param name="func">功能（待执行）</param>
        /// <returns><paramnref name=“func”/>的返回值</returns>
        public static TResult Locking<T, TResult>(this T source, Func<T, TResult> func) where T : class
        {
            lock (source)
            {
                return func(source);
            }
        }
    }
}
