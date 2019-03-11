using System;
using System.Collections.Generic;

namespace SharePlatformSystem.EntityHistory
{
    /// <summary>
    /// 用于配置实体历史记录。
    /// </summary>
    public interface IEntityHistoryConfiguration
    {
        /// <summary>
        ///用于启用/禁用实体历史记录系统。
        ///默认值：真。设置为false以完全禁用它。
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        ///如果当前用户未登录，则设置为true以启用保存实体历史记录。
        ///默认值：false。
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// 用于选择应作为默认跟踪的类/接口的选择器列表。
        /// </summary>
        IEntityHistorySelectorList Selectors { get; }

        /// <summary>
        /// 忽略用于实体历史记录跟踪的序列化类型。
        /// </summary>
        List<Type> IgnoredTypes { get; }
    }
}
