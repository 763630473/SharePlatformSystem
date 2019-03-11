namespace SharePlatformSystem.Core.ObjectMapping
{
    /// <summary>
    /// 定义映射对象的简单接口。
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// 将一个对象转换为另一个对象。创建 <see cref="TDestination"/>新的对象.
        /// </summary>
        /// <typeparam name="TDestination">目标对象的类型</typeparam>
        /// <param name="source">源对象</param>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// 执行从源对象到现有目标对象的映射
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目的地类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>映射操作后返回相同的对象<see cref="destination"/></returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
