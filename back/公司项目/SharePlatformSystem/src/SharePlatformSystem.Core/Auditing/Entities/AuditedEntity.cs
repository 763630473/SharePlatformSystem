using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    ///“auditedentity tprimarykey”的快捷方式，用于大多数使用的主键类型“string”。
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity: AuditedEntity<string>,IEntity<string>  
    {

    }

    /// <summary>
    /// 此类可用于简化实现“iaudited”。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited<TPrimaryKey>
    {
        /// <summary>
        /// 最后一次修改日期
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改实体的用户
        /// </summary>
        public virtual TPrimaryKey LastModifierUserId { get; set; }
    }

    /// <summary>
    /// 此类可用于简化实现“iaudited tuser”。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    /// <typeparam name="TUser">修改用户的类型</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TUser, TPrimaryKey> : AuditedEntity<TPrimaryKey>,IAudited<TUser, TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 创建实体的用户
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// 最后修改实体的用户
        /// </summary>
        [ForeignKey("LastModifierUserId")]
        public virtual TUser LastModifierUser { get; set; }
    }
}
