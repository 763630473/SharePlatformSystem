using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ��DTO����ֱ��ʹ�ã���̳У�
    /// ����Ϊ�յ�IDֵ���ݸ�Ӧ�ó�����񷽷���
    /// </summary>
    /// <typeparam name="TId">����������</typeparam>
    [Serializable]
    public class NullableIdDto<TId>
        where TId : struct
    {
        public TId? Id { get; set; }

        public NullableIdDto()
        {

        }

        public NullableIdDto(TId? id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// "NullableIdDto{TId}"/> ��int�Ŀ��ʵ�ַ�ʽ��
    /// </summary>
    [Serializable]
    public class NullableIdDto : NullableIdDto<int>
    {
        public NullableIdDto()
        {

        }

        public NullableIdDto(int? id)
            : base(id)
        {

        }
    }
}