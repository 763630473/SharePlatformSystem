using System;

namespace SharePlatformSystem.Framework.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class DontWrapResultAttribute : WrapResultAttribute
    {
     
        public DontWrapResultAttribute()
            : base(false, false)
        {

        }
    }
}