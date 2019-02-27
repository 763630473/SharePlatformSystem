using System.Collections.Generic;
using SharePlatformSystem.Framework.Api.ProxyScripting.Configuration;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    /// <summary>
    /// Used to configure ABP Web Common module.
    /// </summary>
    public interface ISharePlatformWebCommonModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (ABP hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

        /// <summary>
        /// Used to configure Api proxy scripting.
        /// </summary>
        IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        /// <summary>
        /// Used to configure embedded resource system for web applications.
        /// </summary>
        IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }
    }
}