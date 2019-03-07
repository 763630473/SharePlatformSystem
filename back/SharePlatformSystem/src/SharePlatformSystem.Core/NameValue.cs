using System;

namespace SharePlatformSystem
{
    /// <summary>
    ///�����ڴ洢����/ֵ�����/ֵ���ԡ�
    /// </summary>
    [Serializable]
    public class NameValue : NameValue<string>
    {
        /// <summary>
        /// �����µġ�NameValue��
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// �����µġ�NameValue��
        /// </summary>
        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    ///�����ڴ洢����/ֵ�����/ֵ���ԡ�
    /// </summary>
    [Serializable]
    public class NameValue<T>
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// �����µġ�NameValue��
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// �����µġ�NameValue��
        /// </summary>
        public NameValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}