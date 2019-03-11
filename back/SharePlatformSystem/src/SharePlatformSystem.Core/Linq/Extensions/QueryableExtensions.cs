using System;
using System.Linq;
using System.Linq.Expressions;
using SharePlatformSystem.Applications.Services.Dto;

namespace SharePlatformSystem.Linq.Extensions
{
    /// <summary>
    /// 一些有用的扩展方法<see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 用于分页。可以用作skip（…）的替代方法。take（…）链接。
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }

        /// <summary>
        /// 用于与<see cref="ipagedResultRequest"/>对象进行分页。
        /// </summary>
        /// <param name="query">可查询以应用分页</param>
        /// <param name="pagedResultRequest">对象实现<see cref="IPagedResultRequest"/></param>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, IPagedResultRequest pagedResultRequest)
        {
            return query.PageBy(pagedResultRequest.SkipCount, pagedResultRequest.MaxResultCount);
        }

        /// <summary>
        /// 如果给定条件为真，则按给定谓词筛选。<see cref="IQueryable{T}"/> 
        /// </summary>
        /// <param name="query">可查询应用筛选</param>
        /// <param name="condition">布尔值</param>
        /// <param name="predicate">筛选查询的谓词</param>
        /// <returns>基于<paramref name="condition"/>筛选或未筛选的查询</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        /// <summary>
        /// 如果给定条件为真，则按给定谓词筛选<see cref="IQueryable{T}"/> .
        /// </summary>
        /// <param name="query">可查询应用筛选</param>
        /// <param name="condition">布尔值</param>
        /// <param name="predicate">筛选查询的谓词</param>
        /// <returns>基于<paramref name="condition"/>筛选或未筛选的查询</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
