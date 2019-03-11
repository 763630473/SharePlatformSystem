using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    ///用于获取任何可以使用“iocmanager.instance”解析的类的单例。
    ///重要提示：尽可能通过注入来使用类。此类用于不可能的情况。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonDependency<T>
    {
        /// <summary>
        /// 获取实例。
        /// </summary>
        /// <value>
        ///实例.
        /// </value>
        public static T Instance => LazyInstance.Value;
        private static readonly Lazy<T> LazyInstance;

        static SingletonDependency()
        {
            LazyInstance = new Lazy<T>(() => IocManager.Instance.Resolve<T>(), true);
        }
    }
}
