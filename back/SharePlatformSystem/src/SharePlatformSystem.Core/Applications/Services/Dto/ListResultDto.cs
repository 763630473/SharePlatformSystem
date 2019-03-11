using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ʵ��"IListResult{T}".
    /// </summary>
    /// <typeparam name="T">���͵Ķ����ڡ�Items���б�</typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <summary>
        /// �б����
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        ///����һ���µ�"ListResultDto{T}"����.
        /// </summary>
        public ListResultDto()
        {
            
        }

        /// <summary>
        /// ����һ���µ�"ListResultDto{T}"����.
        /// </summary>
        /// <param name="items">List of items</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}