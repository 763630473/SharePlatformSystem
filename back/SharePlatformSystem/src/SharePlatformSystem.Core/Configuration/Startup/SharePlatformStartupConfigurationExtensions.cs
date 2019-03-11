using System;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Configuration.Startup
{
    /// <summary>
    /// “ishareplatformstartupconfiguration”的扩展方法。
    /// </summary>
    public static class SharePlatformStartupConfigurationExtensions
    {
        /// <summary>
        ///用于替换服务类型。
        /// </summary>
        /// <param name="configuration">配置。</param>
        /// <param name="type">类型</param>
        /// <param name="impl">实施。</param>
        /// <param name="lifeStyle">生命方式。</param>
        public static void ReplaceService(this ISharePlatformStartupConfiguration configuration, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            configuration.ReplaceService(type, () =>
            {
                configuration.IocManager.Register(type, impl, lifeStyle);
            });
        }

        /// <summary>
        /// 用于替换服务类型。
        /// </summary>
        /// <typeparam name="TType">服务的类型。</typeparam>
        /// <typeparam name="TImpl">实现的类型。</typeparam>
        /// <param name="configuration">配置。</param>
        /// <param name="lifeStyle">生命样式</param>
        public static void ReplaceService<TType, TImpl>(this ISharePlatformStartupConfiguration configuration, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            configuration.ReplaceService(typeof(TType), () =>
            {
                configuration.IocManager.Register<TType, TImpl>(lifeStyle);
            });
        }


        /// <summary>
        /// 用于替换服务类型。
        /// </summary>
        /// <typeparam name="TType">服务的类型</typeparam>
        /// <param name="configuration">配置。</param>
        /// <param name="replaceAction">替换动作。</param>
        public static void ReplaceService<TType>(this ISharePlatformStartupConfiguration configuration, Action replaceAction)
            where TType : class
        {
            configuration.ReplaceService(typeof(TType), replaceAction);
        }
    }
}