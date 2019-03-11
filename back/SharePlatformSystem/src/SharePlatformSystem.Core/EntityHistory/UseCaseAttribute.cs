using System;

namespace SharePlatformSystem.EntityHistory
{
    /// <summary>
    /// 此属性用于为单个方法或
    ///类或接口的所有方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UseCaseAttribute : Attribute
    {
        public string Description { get; set; }
    }
}
