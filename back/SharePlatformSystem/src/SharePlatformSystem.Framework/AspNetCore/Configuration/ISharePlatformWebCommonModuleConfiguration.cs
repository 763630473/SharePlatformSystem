using System.Collections.Generic;
using SharePlatformSystem.Framework.Api.ProxyScripting.Configuration;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    /// <summary>
    /// 用于配置SharePlatform Web公共模块。
    /// </summary>
    public interface ISharePlatformWebCommonModuleConfiguration
    {
        /// <summary>
        ///如果设置为true，则在出现错误时，所有异常和详细信息都将直接发送到客户端。
        ///默认值：false（abp从客户端隐藏异常详细信息，特殊异常除外。）
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

        /// <summary>
        /// 用于配置API代理脚本。
        /// </summary>
        IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        /// <summary>
        /// 用于为Web应用程序配置嵌入式资源系统。
        /// </summary>
        IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }
    }
}