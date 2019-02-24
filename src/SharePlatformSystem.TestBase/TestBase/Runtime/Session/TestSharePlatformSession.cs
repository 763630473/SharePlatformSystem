using SharePlatformSystem.Dependency;
using SharePlatformSystem.Runtime;
using SharePlatformSystem.Runtime.Session;
using System;
namespace SharePlatformSystem.TestBase.Runtime.Session
{
    public class TestSharePlatformSession : ISharePlatformSession, ISingletonDependency
    {
        public virtual string UserId
        {
            get
            {
                if (_sessionOverrideScopeProvider.GetValue(SharePlatformSessionBase.SessionOverrideContextKey) != null)
                {
                    return _sessionOverrideScopeProvider.GetValue(SharePlatformSessionBase.SessionOverrideContextKey).UserId;
                }

                return _userId;
            }
            set { _userId = value; }
        }      
        
        public virtual string ImpersonatorUserId { get; set; }
        
        public virtual int? ImpersonatorTenantId { get; set; }

        private readonly IAmbientScopeProvider<SessionOverride> _sessionOverrideScopeProvider;
        private string _userId;

        public TestSharePlatformSession(
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        {
            _sessionOverrideScopeProvider = sessionOverrideScopeProvider;
        }   

        public virtual IDisposable Use(string userId)
        {
            return _sessionOverrideScopeProvider.BeginScope(SharePlatformSessionBase.SessionOverrideContextKey, new SessionOverride(userId));
        }
    }
}