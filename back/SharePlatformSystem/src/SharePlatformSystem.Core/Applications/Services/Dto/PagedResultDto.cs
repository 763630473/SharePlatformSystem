using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ʵ��"IPagedResult{T}"�ӿ�.
    /// </summary>
    /// <typeparam name="T"> "ListResultDto{T}.Items" �б�Item������</typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        /// <summary>
        /// ��Ŀ������
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// ����һ���µġ�pagedresuldto t������
        /// </summary>
        public PagedResultDto()
        {

        }

        /// <summary>
        ///����һ���µġ�pagedresuldto t������
        /// </summary>
        /// <param name="totalCount">��Ŀ����</param>
        /// <param name="items">��ǰҳ�е���Ŀ�б�</param>
        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }
    }
}