using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.ObjectMapping
{
    public sealed class NullObjectMapper : IObjectMapper, ISingletonDependency
    {
        /// <summary>
        /// ����ʵ����
        /// </summary>
        public static NullObjectMapper Instance { get; } = new NullObjectMapper();

        public TDestination Map<TDestination>(object source)
        {
            throw new SharePlatformException("Ϊ��ӳ�����Ӧʵ��SharePlatform.ObjectMapping.IObjectMapper��");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new SharePlatformException("Ϊ��ӳ�����Ӧʵ��SharePlatform.ObjectMapping.IObjectMapper��");
        }
    }
}
