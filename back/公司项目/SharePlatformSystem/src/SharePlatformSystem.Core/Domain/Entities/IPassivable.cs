using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 此接口用于使实体成为主动/被动的。
    /// </summary>
    public interface IPassivable
    {
        /// <summary>
        ///true：这是真正的实体的活动。
        ///false：这个实体是不活跃的。
        /// </summary>
        bool IsActive { get; set; }
    }
}