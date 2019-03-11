using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using SharePlatformSystem.EntityHistory;
using SharePlatformSystem.Runtime;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Framework.AspNetCore.EntityHistory
{
    /// <summary>
    /// implements<see cref=“IEntityChangeSetReasonProvider”/>以从HTTP请求获取原因。
    /// </summary>
    public class HttpRequestEntityChangeSetReasonProvider : EntityChangeSetReasonProviderBase, ISingletonDependency
    {
        [CanBeNull]
        public override string Reason
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.Reason;
                }

                try
                {
                    return HttpContextAccessor.HttpContext?.Request.GetDisplayUrl();
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }

        protected IHttpContextAccessor HttpContextAccessor { get; }

        public HttpRequestEntityChangeSetReasonProvider(
            IHttpContextAccessor httpContextAccessor,

            IAmbientScopeProvider<ReasonOverride> reasonOverrideScopeProvider
            ) : base(reasonOverrideScopeProvider)
        {
            HttpContextAccessor = httpContextAccessor;
        }
    }
}
