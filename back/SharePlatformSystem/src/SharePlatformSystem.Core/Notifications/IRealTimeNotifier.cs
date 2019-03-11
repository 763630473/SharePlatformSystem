using System.Threading.Tasks;

namespace SharePlatformSystem.Notifications
{
    /// <summary>
    /// 向用户发送实时通知的界面。
    /// </summary>
    public interface IRealTimeNotifier
    {
        /// <summary>
        /// 此方法尝试将实时通知传递给指定的用户。
        ///如果用户不在线，应该忽略他。
        /// </summary>
        Task SendNotificationsAsync(UserNotification[] userNotifications);
    }
}