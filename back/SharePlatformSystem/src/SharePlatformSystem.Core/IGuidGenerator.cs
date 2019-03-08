using System;

namespace SharePlatformSystem
{
    /// <summary>
    ///用于生成ID。
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        ///创建一个GUID。
        /// </summary>
        Guid Create();
    }
}
