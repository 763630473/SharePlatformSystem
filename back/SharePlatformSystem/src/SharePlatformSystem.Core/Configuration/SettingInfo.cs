using System;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 表示设置信息。
    /// </summary>
    [Serializable]
    public class SettingInfo
    {
        /// <summary>
        ///此设置的用户ID。
        ///如果此设置不是用户级别，则userid为空。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///设置的唯一名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///设置的值。
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 创建新的“settinginfo”对象。
        /// </summary>
        public SettingInfo()
        {
            
        }

        /// <summary>
        /// 创建新的“settinginfo”对象。
        /// </summary>    
        /// <param name="userId">此设置的用户ID。如果此设置不是用户级别，则userid为空。</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="value">设置值</param>
        public SettingInfo(string userId, string name, string value)
        {
            UserId = userId;
            Name = name;
            Value = value;
        }
    }
}