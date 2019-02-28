using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    ///此接口由想要存储修改信息的实体实现（最后修改的是谁和何时修改）。
    ///更新“IEntity”时自动设置属性
    /// </summary>
    public interface IModificationAudited<TPrimaryKey> : IHasModificationTime
    {
        /// <summary>
        /// 此实体的最后一个修改用户。
        /// </summary>
        TPrimaryKey LastModifierUserId { get; set; }
    }

    /// <summary>
    ///如果必须存储此实体的“lastmodificationtime”，则实体可以实现此接口。
    ///更新“实体”时自动设置“LastModificationTime”。
    /// </summary>
    /// <typeparam name="TUser">用户实体的类型</typeparam>
    public interface IModificationAudited<TUser, TPrimaryKey>:IModificationAudited<TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 引用此实体的最后一个修改符用户。
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}