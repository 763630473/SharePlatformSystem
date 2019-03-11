using System;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// string的快捷方式
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntityDto : CreationAuditedEntityDto<string>
    {
        
    }

    /// <summary>
    /// 这个类可以为简单的DTO对象继承，这些对象用于实现“ICreationAudited”接口的实体。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, ICreationAudited<TPrimaryKey>
    {
        /// <summary>
        ///创建实体的时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 实体的创建用户
        /// </summary>
        public TPrimaryKey CreatorUserId { get; set; }

        /// <summary>
        /// 构造器.
        /// </summary>
        protected CreationAuditedEntityDto()
        {
            CreationTime = Clock.Now;
        }
    }
}