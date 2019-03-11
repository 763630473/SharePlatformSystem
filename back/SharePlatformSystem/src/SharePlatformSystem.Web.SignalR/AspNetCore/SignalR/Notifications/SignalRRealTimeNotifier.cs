using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.SignalR;
using SharePlatform.AspNetCore.SignalR.Hubs;
using SharePlatformSystem;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Notifications;
using SharePlatformSystem.RealTime;

namespace SharePlatform.AspNetCore.SignalR.Notifications
{
    /// <summary>
    /// 实现通过信号器发送通知。
    /// </summary>
    public class SignalRRealTimeNotifier : IRealTimeNotifier, ITransientDependency
    {
        /// <summary>
        ///对记录器的引用。
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly IOnlineClientManager _onlineClientManager;

        private readonly IHubContext<SharePlatformCommonHub> _hubContext;

        /// <summary>
        ///初始化类的新实例。
        /// </summary>
        public SignalRRealTimeNotifier(
            IOnlineClientManager onlineClientManager,
            IHubContext<SharePlatformCommonHub> hubContext)
        {
            _onlineClientManager = onlineClientManager;
            _hubContext = hubContext;
            Logger = NullLogger.Instance;
        }

        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                try
                {
                    var onlineClients = _onlineClientManager.GetAllByUserId(userNotification);
                    foreach (var onlineClient in onlineClients)
                    {
                        var signalRClient = _hubContext.Clients.Client(onlineClient.ConnectionId);
                        if (signalRClient == null)
                        {
                            Logger.Debug("Can not get user " + userNotification.ToUserIdentifier() + " with connectionId " + onlineClient.ConnectionId + " from SignalR hub!");
                            continue;
                        }

                        signalRClient.SendAsync("getNotification", userNotification);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn("Could not send notification to user: " + userNotification.ToUserIdentifier());
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            return Task.FromResult(0);
        }
    }
}
