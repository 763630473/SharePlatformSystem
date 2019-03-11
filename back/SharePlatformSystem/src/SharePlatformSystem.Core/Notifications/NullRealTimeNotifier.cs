using SharePlatformSystem.Notifications;
using System.Threading.Tasks;

namespace SharePlatformSystem.Notifications
{
    /// <summary>
    /// 空模式实现<see cref="IRealTimeNotifier"/>.
    /// </summary>
    public class NullRealTimeNotifier : IRealTimeNotifier
    {
        /// <summary>
        ///获取<see cref="NullRealTimeNotifier"/>类的实例 .
        /// </summary>
        public static NullRealTimeNotifier Instance { get; } = new NullRealTimeNotifier();

        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            return Task.FromResult(0);
        }
    }
}
