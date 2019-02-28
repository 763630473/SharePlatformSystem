using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// “fullAuditedEntity”的快捷方式，用于大多数使用的主键类型“string”。
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntity : FullAuditedEntity<string>, IEntity<string>
    {

    }

    /// <summary>
    ///实现“ifullAudited”作为完全审计实体的基类。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited<TPrimaryKey>
    {
        /// <summary>
        /// 此实体是否已删除？
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        ///哪个用户删除了此实体？
        /// </summary>
        public virtual TPrimaryKey DeleterUserId { get; set; }

        /// <summary>
        /// 此实体的删除时间。
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// 实现“ifullAudited”作为完全审计实体的基类。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TUser,TPrimaryKey> : AuditedEntity<TUser,TPrimaryKey>, IFullAudited<TUser, TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {
        /// <summary>
        ///此实体是否已删除？
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 引用此实体的删除程序用户。
        /// </summary>
        [ForeignKey("DeleterUserId")]
        public virtual TUser DeleterUser { get; set; }

        /// <summary>
        /// 哪个用户删除了此实体？
        /// </summary>
        public virtual TPrimaryKey DeleterUserId { get; set; }

        /// <summary>
        /// 此实体的删除时间。
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
}