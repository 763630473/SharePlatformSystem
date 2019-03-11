using System;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// 定义一些对应用程序有用的会话信息。
    /// </summary>
    public interface ISharePlatformSession
    {
        /// <summary>
        ///获取当前用户ID或空值。
        ///如果没有用户登录，则可以为空。
        /// </summary>
        string UserId { get; }

        /// <summary>
        ///获取当前的Tenantid或空值。
        ///此tenantid应为<see cref=“userid”/>的tenantid。
        ///如果给定<see cref=“userid”/>是主机用户或没有用户登录，则可以为空。
        /// </summary>

        /// <summary>
        ///模拟程序的用户ID。
        ///如果用户代表用户执行操作，则填写此项。
        /// </summary>
        string ImpersonatorUserId { get; }


        /// <summary>
        ///用于更改有限范围的<see cref=“userid”/>。
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDisposable Use(string userId);
    }
}
