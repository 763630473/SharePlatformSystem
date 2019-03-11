using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 实现"IListResult{T}".
    /// </summary>
    /// <typeparam name="T">类型的对象，在“Items”列表</typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <summary>
        /// 列表对象
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        ///创建一个新的"ListResultDto{T}"对象.
        /// </summary>
        public ListResultDto()
        {
            
        }

        /// <summary>
        /// 创建一个新的"ListResultDto{T}"对象.
        /// </summary>
        /// <param name="items">List of items</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}