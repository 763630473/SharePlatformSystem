﻿using System;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// 此属性用于为单个方法或类或接口的所有方法应用审核日志记录。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class AuditedAttribute : Attribute
    {

    }
}
