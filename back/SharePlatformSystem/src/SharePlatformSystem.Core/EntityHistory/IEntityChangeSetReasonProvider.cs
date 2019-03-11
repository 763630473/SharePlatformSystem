using System;

namespace SharePlatformSystem.EntityHistory
{
    /// <summary>
    /// 定义一些对应用程序有用的会话信息。
    /// </summary>
    public interface IEntityChangeSetReasonProvider
    {
        /// <summary>
        /// 获取当前原因或空值。
        /// </summary>
        string Reason { get; }

        /// <summary>
        ///用于更改<see cref=“reason”/>了解有限范围。
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        IDisposable Use(string reason);
    }
}
