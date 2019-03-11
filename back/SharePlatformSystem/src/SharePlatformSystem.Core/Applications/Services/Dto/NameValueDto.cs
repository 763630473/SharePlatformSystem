using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 可用于发送/接收名称/值（或键/值）对。
    /// </summary>
    [Serializable]
    public class NameValueDto : NameValueDto<string>
    {
        /// <summary>
        /// 创建一个新的"NameValueDto".
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// 创建一个新的"NameValueDto".
        /// </summary>
        public NameValueDto(string name, string value)
            : base(name, value)
        {

        }

        /// <summary>
        /// 创建一个新的"NameValueDto".
        /// </summary>
        /// <param name="nameValue">一个“namevalue”对象来获取它的名称和值</param>
        public NameValueDto(NameValue nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }

    /// <summary>
    /// 可用于发送/接收名称/值（或键/值）对。
    /// </summary>
    [Serializable]
    public class NameValueDto<T> : NameValue<T>
    {
        /// <summary>
        /// 创建一个新的"NameValueDto".
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// 创建一个新的"NameValueDto".
        /// </summary>
        public NameValueDto(string name, T value)
            : base(name, value)
        {

        }

        /// <summary>
        /// 创建一个新的"NameValueDto".
        /// </summary>
        /// <param name="nameValue">一个“namevalue”对象来获取它的名称和值</param>
        public NameValueDto(NameValue<T> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }
}