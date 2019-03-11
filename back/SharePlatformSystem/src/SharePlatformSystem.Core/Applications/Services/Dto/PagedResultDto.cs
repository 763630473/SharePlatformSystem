using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 实现"IPagedResult{T}"接口.
    /// </summary>
    /// <typeparam name="T"> "ListResultDto{T}.Items" 列表Item的类型</typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        /// <summary>
        /// 项目总数。
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 创建一个新的“pagedresuldto t”对象。
        /// </summary>
        public PagedResultDto()
        {

        }

        /// <summary>
        ///创建一个新的“pagedresuldto t”对象。
        /// </summary>
        /// <param name="totalCount">项目总数</param>
        /// <param name="items">当前页中的项目列表</param>
        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }
    }
}