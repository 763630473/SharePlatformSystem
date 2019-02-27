using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Domain.Uow
{
    /// <summary>
    /// 标准过滤器。
    /// </summary>
    public static class SharePlatformDataFilters
    {
        /// <summary>
        /// 软删除
        /// 软删除过滤器
        /// 阻止从数据库中获取已删除的数据。
        /// </summary>
        public const string SoftDelete = "SoftDelete";

        /// <summary>
        /// 标准参数。
        /// </summary>
        public static class Parameters
        {          
            /// <summary>
            /// 是否删除
            /// </summary>
            public const string IsDeleted = "isDeleted";
        }
    }
}