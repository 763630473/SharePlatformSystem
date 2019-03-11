using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// 用于配置审核。
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// 用于启用/禁用审核系统。
        /// 默认: true.设置为false以完全禁用它。
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 如果当前用户未登录，则设置为true以启用保存审核日志。
        /// 默认: false.
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// 用于选择应作为默认值审核的类/接口的选择器列表。
        /// </summary>
        IAuditingSelectorList Selectors { get; }

        /// <summary>
        /// 在审核日志记录中忽略用于序列化的类型。
        /// </summary>
        List<Type> IgnoredTypes { get; }
    }
}