using SharePlatformSystem.AspNetCore.SignalR.Hubs;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.RealTime;

namespace SharePlatform.AspNetCore.SignalR.Hubs
{
    public class SharePlatformCommonHub : OnlineClientHubBase
    {
        public SharePlatformCommonHub(IOnlineClientManager onlineClientManager, IClientInfoProvider clientInfoProvider) 
            : base(onlineClientManager, clientInfoProvider)
        {
        }

        public void Register()
        {
            Logger.Debug("A client is registered: " + Context.ConnectionId);
        }
    }
}
