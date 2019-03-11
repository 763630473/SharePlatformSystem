using System;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// string�Ŀ�ݷ�ʽ
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntityDto : CreationAuditedEntityDto<string>
    {
        
    }

    /// <summary>
    /// ��������Ϊ�򵥵�DTO����̳У���Щ��������ʵ�֡�ICreationAudited���ӿڵ�ʵ�塣
    /// </summary>
    /// <typeparam name="TPrimaryKey">ʵ�����������</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, ICreationAudited<TPrimaryKey>
    {
        /// <summary>
        ///����ʵ���ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// ʵ��Ĵ����û�
        /// </summary>
        public TPrimaryKey CreatorUserId { get; set; }

        /// <summary>
        /// ������.
        /// </summary>
        protected CreationAuditedEntityDto()
        {
            CreationTime = Clock.Now;
        }
    }
}