using SharePlatformSystem.Core.Auditing.Entities;
using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 最常用的主键类型string的快捷方式
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityDto : AuditedEntityDto<string>
    {

    }

    /// <summary>
    /// 这个类可以为简单的DTO对象继承，这些对象用于实现“iaudited_tuser”接口的实体。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键的类型</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited<TPrimaryKey>
    {
        /// <summary>
        /// 实体最后被修改的时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 记录最后修改的用户
        /// </summary>
        public TPrimaryKey LastModifierUserId { get; set; }
    }
}