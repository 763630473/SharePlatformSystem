using System;
using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.Runtime.Session
{
    public abstract class SharePlatformSessionBase : ISharePlatformSession
    {
        public const string SessionOverrideContextKey = "SharePlatform.Runtime.Session.Override";


        public abstract string UserId { get; }


        public abstract string ImpersonatorUserId { get; }



        protected SessionOverride OverridedValue => SessionOverrideScopeProvider.GetValue(SessionOverrideContextKey);
        protected IAmbientScopeProvider<SessionOverride> SessionOverrideScopeProvider { get; }

        protected SharePlatformSessionBase(IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        {
            SessionOverrideScopeProvider = sessionOverrideScopeProvider;
        }

        public IDisposable Use(string userId)
        {
            return SessionOverrideScopeProvider.BeginScope(SessionOverrideContextKey, new SessionOverride(userId));
        }
    }
}