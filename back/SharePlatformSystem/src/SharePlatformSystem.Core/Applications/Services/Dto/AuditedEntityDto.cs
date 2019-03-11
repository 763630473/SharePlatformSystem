using SharePlatformSystem.Core.Auditing.Entities;
using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ��õ���������string�Ŀ�ݷ�ʽ
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityDto : AuditedEntityDto<string>
    {

    }

    /// <summary>
    /// ��������Ϊ�򵥵�DTO����̳У���Щ��������ʵ�֡�iaudited_tuser���ӿڵ�ʵ�塣
    /// </summary>
    /// <typeparam name="TPrimaryKey">����������</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited<TPrimaryKey>
    {
        /// <summary>
        /// ʵ������޸ĵ�ʱ��
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// ��¼����޸ĵ��û�
        /// </summary>
        public TPrimaryKey LastModifierUserId { get; set; }
    }
}