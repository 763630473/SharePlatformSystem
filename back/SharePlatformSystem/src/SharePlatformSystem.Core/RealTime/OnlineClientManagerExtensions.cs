using System.Linq;
using JetBrains.Annotations;
using SharePlatformSystem.Core;

namespace SharePlatformSystem.RealTime
{
    /// <summary>
    ///<see cref="IOnlineClientManager"/>的扩展方法.
    /// </summary>
    public static class OnlineClientManagerExtensions
    {
        /// <summary>
        /// 确定指定的用户是否联机。
        /// </summary>
        /// <param name="onlineClientManager">在线客户管理。</param>
        /// <param name="user">User.</param>
        public static bool IsOnline(
            [NotNull] this IOnlineClientManager onlineClientManager,
            [NotNull] UserIdentifier user)
        {
            return onlineClientManager.GetAllByUserId(user).Any();
        }

        public static bool Remove(
            [NotNull] this IOnlineClientManager onlineClientManager,
            [NotNull] IOnlineClient client)
        {
            Check.NotNull(onlineClientManager, nameof(onlineClientManager));
            Check.NotNull(client, nameof(client));

            return onlineClientManager.Remove(client.ConnectionId);
        }
    }
}