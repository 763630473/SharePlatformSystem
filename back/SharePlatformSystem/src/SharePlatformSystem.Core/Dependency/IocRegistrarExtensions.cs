using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// “IIocRegistrar”接口的扩展方法。
    /// </summary>
    public static class IocRegistrarExtensions
    {
        #region RegisterIfNot

        /// <summary>
        /// 如果类型以前未注册，则将其注册为自注册。
        /// </summary>
        /// <typeparam name="T">类的类型</typeparam>
        /// <param name="iocRegistrar">注册器</param>
        /// <param name="lifeStyle">对象生命周期的类型</param>
        /// <returns>如果为给定的实现注册，则为true。</returns>
        public static bool RegisterIfNot<T>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class
        {
            if (iocRegistrar.IsRegistered<T>())
            {
                return false;
            }

            iocRegistrar.Register<T>(lifeStyle);
            return true;
        }

        /// <summary>
        /// 如果类型以前未注册，则将其注册为自注册。
        /// </summary>
        /// <param name="iocRegistrar">注册器</param>
        /// <param name="lifeStyle">对象生命周期的类型</param>
        /// <returns>如果为给定的实现注册，则为true。</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, lifeStyle);
            return true;
        }

        /// <summary>
        /// Registers a type with it's implementation if it's not registered before.
        /// </summary>
        /// <typeparam name="TType">注册的类型</typeparam>
        /// <typeparam name="TImpl">实现的类型"TType"</typeparam>
        /// <param name="iocRegistrar">注册器</param>
        /// <param name="lifeStyle">对象生命周期的类型</param>
        /// <returns>如果为给定的实现注册，则为true。</returns>
        public static bool RegisterIfNot<TType, TImpl>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return false;
            }

            iocRegistrar.Register<TType, TImpl>(lifeStyle);
            return true;
        }


        /// <summary>
        /// 如果以前没有注册过类型，则在其实现中注册该类型。
        /// </summary>
        /// <param name="iocRegistrar">注册器</param>
        /// <param name="lifeStyle">对象生命周期的类型</param>
        /// <returns>如果为给定的实现注册，则为true。</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, impl, lifeStyle);
            return true;
        }

        #endregion
    }
}