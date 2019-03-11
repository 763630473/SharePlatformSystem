using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.ObjectMapping
{
    public sealed class NullObjectMapper : IObjectMapper, ISingletonDependency
    {
        /// <summary>
        /// 单例实例。
        /// </summary>
        public static NullObjectMapper Instance { get; } = new NullObjectMapper();

        public TDestination Map<TDestination>(object source)
        {
            throw new SharePlatformException("为了映射对象，应实现SharePlatform.ObjectMapping.IObjectMapper。");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new SharePlatformException("为了映射对象，应实现SharePlatform.ObjectMapping.IObjectMapper。");
        }
    }
}
