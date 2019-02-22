using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    ///如果必须存储此实体的creatintime，则实体可以实现此接口。
    ///在将实体保存到数据库时，会自动设置creationTime。
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 此实体的创建时间。
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}