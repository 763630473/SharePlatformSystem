using System;
using SharePlatformSystem.Core.Auditing.Entities;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ��õ���������string�Ŀ�ݷ�ʽ.
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntityDto : FullAuditedEntityDto<string>
    {

    }

    /// <summary>
    /// ��������ʵ��ʵ�ֵļ�DTO���󣬿��Լ̳д��ࡰifullAudited tuser�ӿڡ�
    /// </summary>
    /// <typeparam name="TPrimaryKey">����������</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited<TPrimaryKey>
    {
        /// <summary>
        /// ��ʵ���Ƿ���ɾ����
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// ɾ���û�ID�����ɾ����ʵ�壬
        /// </summary>
        public TPrimaryKey DeleterUserId { get; set; }

        /// <summary>
        ///ɾ��ʱ�䣬���ɾ����ʵ�壬
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}