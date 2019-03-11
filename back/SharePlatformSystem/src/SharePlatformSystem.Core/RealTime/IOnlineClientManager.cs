using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharePlatformSystem.RealTime
{
    /// <summary>
    /// 用于管理连接到应用程序的联机客户端。
    /// </summary>
    public interface IOnlineClientManager<T> : IOnlineClientManager
    {

    }

    public interface IOnlineClientManager
    {
        event EventHandler<OnlineClientEventArgs> ClientConnected;

        event EventHandler<OnlineClientEventArgs> ClientDisconnected;

        event EventHandler<OnlineUserEventArgs> UserConnected;

        event EventHandler<OnlineUserEventArgs> UserDisconnected;

        /// <summary>
        /// 添加客户端。
        /// </summary>
        /// <param name="client">The client.</param>
        void Add(IOnlineClient client);

        /// <summary>
        /// 按连接ID删除客户端。
        /// </summary>
        /// <param name="connectionId">连接ID。</param>
        /// <returns>如果删除了客户端，则为true</returns>
        bool Remove(string connectionId);

        /// <summary>
        ///尝试通过连接ID查找客户端。
        ///如果找不到，则返回空值。
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        IOnlineClient GetByConnectionIdOrNull(string connectionId);

        /// <summary>
        /// 获取所有联机客户端。
        /// </summary>
        IReadOnlyList<IOnlineClient> GetAllClients();

        IReadOnlyList<IOnlineClient> GetAllByUserId([NotNull] IUserIdentifier user);
    }
}