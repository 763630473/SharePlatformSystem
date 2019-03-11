using System;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// 用于对单个方法或类或接口的所有方法禁用审核。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {

    }
}