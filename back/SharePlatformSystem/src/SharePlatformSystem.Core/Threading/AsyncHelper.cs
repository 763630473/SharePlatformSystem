using System;
using System.Reflection;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// 提供一些帮助器方法来使用异步方法。
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// 检查给定的方法是否为异步方法。
        /// </summary>
        /// <param name="method">一种检查方法</param>
        public static bool IsAsync(this MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.GetTypeInfo().IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            );
        }

        /// <summary>
        /// 检查给定的方法是否为异步方法。
        /// </summary>
        /// <param name="method">一种检查方法</param>
        [Obsolete("Use MethodInfo.IsAsync() extension method!")]
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return method.IsAsync();
        }

        /// <summary>
        /// 同步运行异步方法。
        /// </summary>
        /// <param name="func">返回结果的函数</param>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <returns>异步操作的结果</returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncContext.Run(func);
        }

        /// <summary>
        ///同步运行异步方法。
        /// </summary>
        /// <param name="action">异步动作</param>
        public static void RunSync(Func<Task> action)
        {
            AsyncContext.Run(action);
        }
    }
}
