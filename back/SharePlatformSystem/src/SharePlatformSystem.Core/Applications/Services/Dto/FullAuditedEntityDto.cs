using System;
using SharePlatformSystem.Core.Auditing.Entities;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 最常用的主键类型string的快捷方式.
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntityDto : FullAuditedEntityDto<string>
    {

    }

    /// <summary>
    /// 对于用于实体实现的简单DTO对象，可以继承此类“ifullAudited tuser接口。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键的类型</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited<TPrimaryKey>
    {
        /// <summary>
        /// 此实体是否已删除？
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除用户ID，如果删除此实体，
        /// </summary>
        public TPrimaryKey DeleterUserId { get; set; }

        /// <summary>
        ///删除时间，如果删除此实体，
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}