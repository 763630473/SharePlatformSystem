using System;
using System.Collections.Generic;

namespace SharePlatformSystem.RealTime
{
    /// <summary>
    /// 表示连接到应用程序的联机客户端。
    /// </summary>
    public interface IOnlineClient
    {
        /// <summary>
        /// 此客户端的唯一连接ID。
        /// </summary>
        string ConnectionId { get; }

        /// <summary>
        ///此客户端的IP地址。
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// User Id.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 此客户端的连接建立时间。
        /// </summary>
        DateTime ConnectTime { get; }

        /// <summary>
        /// 设置/获取的快捷方式 <see cref="Properties"/>.
        /// </summary>
        object this[string key] { get; set; }

        /// <summary>
        /// 可用于添加此客户端的自定义属性。
        /// </summary>
        Dictionary<string, object> Properties { get; }
    }
}