using SharePlatformSystem.Dependency;
using System;

namespace SharePlatformSystem
{
    /// <summary>
    /// 使用“guid.newguid”实现“iguidGenerator”。
    /// </summary>
    public class RegularGuidGenerator : IGuidGenerator, ITransientDependency
    {
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}