using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// �����ڷ���/��������/ֵ�����/ֵ���ԡ�
    /// </summary>
    [Serializable]
    public class NameValueDto : NameValueDto<string>
    {
        /// <summary>
        /// ����һ���µ�"NameValueDto".
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// ����һ���µ�"NameValueDto".
        /// </summary>
        public NameValueDto(string name, string value)
            : base(name, value)
        {

        }

        /// <summary>
        /// ����һ���µ�"NameValueDto".
        /// </summary>
        /// <param name="nameValue">һ����namevalue����������ȡ�������ƺ�ֵ</param>
        public NameValueDto(NameValue nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }

    /// <summary>
    /// �����ڷ���/��������/ֵ�����/ֵ���ԡ�
    /// </summary>
    [Serializable]
    public class NameValueDto<T> : NameValue<T>
    {
        /// <summary>
        /// ����һ���µ�"NameValueDto".
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// ����һ���µ�"NameValueDto".
        /// </summary>
        public NameValueDto(string name, T value)
            : base(name, value)
        {

        }

        /// <summary>
        /// ����һ���µ�"NameValueDto".
        /// </summary>
        /// <param name="nameValue">һ����namevalue����������ȡ�������ƺ�ֵ</param>
        public NameValueDto(NameValue<T> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }
}