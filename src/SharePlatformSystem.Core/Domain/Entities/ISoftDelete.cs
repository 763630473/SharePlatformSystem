using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    ///用于标准化软删除实体。
    ///实际上没有删除软删除实体，
    ///在数据库中标记为isDeleted=true，
    ///但无法检索到应用程序。
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 用于将实体标记为“已删除”。
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
