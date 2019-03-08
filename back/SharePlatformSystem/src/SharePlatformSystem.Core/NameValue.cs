using System;

namespace SharePlatformSystem
{
    /// <summary>
    ///可用于存储名称/值（或键/值）对。
    /// </summary>
    [Serializable]
    public class NameValue : NameValue<string>
    {
        /// <summary>
        /// 创建新的“NameValue”
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// 创建新的“NameValue”
        /// </summary>
        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    ///可用于存储名称/值（或键/值）对。
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
        /// 创建新的“NameValue”
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// 创建新的“NameValue”
        /// </summary>
        public NameValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}