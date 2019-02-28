using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// 如果必须存储此实体的“LastModificationTime”，则实体可以实现此接口。
    ///更新“实体”时自动设置“LastModificationTime”。
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        ///此实体的上次修改时间。
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}