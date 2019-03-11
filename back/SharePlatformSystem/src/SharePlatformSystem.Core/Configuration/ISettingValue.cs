namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 表示设置的值。
    /// </summary>
    public interface ISettingValue
    {
        /// <summary>
        /// 设置的唯一名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 设置的值。
        /// </summary>
        string Value { get; }
    }
}