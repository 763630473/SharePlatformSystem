using SharePlatformSystem.Runtime;
using System;

namespace SharePlatformSystem.EntityHistory
{
    public abstract class EntityChangeSetReasonProviderBase : IEntityChangeSetReasonProvider
    {
        public const string ReasonOverrideContextKey = "SharePlatform.EntityHistory.Reason.Override";

        public abstract string Reason { get; }

        protected ReasonOverride OverridedValue => ReasonOverrideScopeProvider.GetValue(ReasonOverrideContextKey);
        protected IAmbientScopeProvider<ReasonOverride> ReasonOverrideScopeProvider { get; }

        protected EntityChangeSetReasonProviderBase(IAmbientScopeProvider<ReasonOverride> reasonOverrideScopeProvider)
        {
            ReasonOverrideScopeProvider = reasonOverrideScopeProvider;
        }

        public IDisposable Use(string reason)
        {
            return ReasonOverrideScopeProvider.BeginScope(ReasonOverrideContextKey, new ReasonOverride(reason));
        }
    }
}
