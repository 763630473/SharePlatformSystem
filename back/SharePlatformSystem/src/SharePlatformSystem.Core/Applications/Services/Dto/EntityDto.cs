using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ����������string�Ŀ��ʵ�ַ�ʽ
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<string>, IEntityDto
    {
        /// <summary>
        /// ����һ���µĶ���
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// ����һ���µĴ��������¶���
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(string id)
            : base(id)
        {
        }
    }

    /// <summary>
    /// ʵ�ֻ���ʵ���DTO�Ĺ������ԡ�
    /// </summary>
    /// <typeparam name="TPrimaryKey">����������</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// ʵ�������ID
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// ����һ���µĿ�ʵ�����
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// ����һ���µĴ�������ʵ�����.
        /// </summary>
        /// <param name="id">ʵ�������ID</param>
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}