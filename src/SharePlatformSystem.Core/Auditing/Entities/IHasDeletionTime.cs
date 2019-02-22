using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// 如果必须存储实体的“删除时间”，则实体可以实现此接口。
    ///删除“实体”时自动设置“删除时间”。
    /// </summary>
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>
        /// 此实体的删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}