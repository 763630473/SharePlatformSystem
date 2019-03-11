using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此DTO可以直接使用（或继承）
    /// 将可为空的ID值传递给应用程序服务方法。
    /// </summary>
    /// <typeparam name="TId">主键的类型</typeparam>
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
    /// "NullableIdDto{TId}"/> 的int的快捷实现方式。
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