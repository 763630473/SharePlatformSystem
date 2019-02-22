using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// “creationauditedentity”的快捷方式，用于大多数使用的主键类型“string”。
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntity : CreationAuditedEntity<string>, IEntity<string>
    {

    }

    /// <summary>
    /// 此类可用于简化实现“icreationaudited”。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>,ICreationAudited<TPrimaryKey>
    {
        /// <summary>
        ///此实体的创建时间。
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 此实体的创建者。
        /// </summary>
        public virtual TPrimaryKey CreatorUserId { get; set; }

        /// <summary>
        /// 构造器.
        /// </summary>
        protected CreationAuditedEntity()
        {
            CreationTime = Clock.Now;
        }
    }

    /// <summary>
    /// 此类可用于简化实现“icreationaudited tuser”。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TUser, TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, ICreationAudited<TUser, TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 引用此实体的创建者用户。
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }
    }
}