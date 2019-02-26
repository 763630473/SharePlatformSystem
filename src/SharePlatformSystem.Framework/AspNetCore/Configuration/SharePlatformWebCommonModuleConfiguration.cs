using SharePlatformSystem.Framework.Api.ProxyScripting.Configuration;
using System.Collections.Generic;
namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    internal class SharePlatformWebCommonModuleConfiguration : ISharePlatformWebCommonModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }

        public IApiProxyScriptingConfiguration ApiProxyScripting { get; }


        public IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }

        public SharePlatformWebCommonModuleConfiguration(
            IApiProxyScriptingConfiguration apiProxyScripting,          
            IWebEmbeddedResourcesConfiguration embeddedResources)
        {
            ApiProxyScripting = apiProxyScripting;
            EmbeddedResources = embeddedResources;
        }
    }
}