using System.Globalization;
using AutoMapper;
using System.Linq;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.AutoMapper
{
    public static class AutoMapExtensions
    {
        /// <summary>
        ///使用automapper库将一个对象转换为另一个对象。创建<typeparamref name=“tDestination”/>的新对象。
        ///在调用此方法之前，对象之间必须有映射。
        /// </summary>
        /// <typeparam name="TDestination">目标对象的类型</typeparam>
        /// <param name="source">源对象</param>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        ///执行从源对象到现有目标对象的映射
        ///在调用此方法之前，对象之间必须有映射。
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标对象的类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
