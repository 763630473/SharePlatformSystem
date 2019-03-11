using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Framework.Api.ProxyScripting.Configuration
{
    public interface IApiProxyScriptingConfiguration
    {
        /// <summary>
        /// 用于添加/替换代理脚本生成器。
        /// </summary>
        IDictionary<string, Type> Generators { get; }

        /// <summary>
        /// 默认值：真。
        /// </summary>
        bool RemoveAsyncPostfixOnProxyGeneration { get; set; }
    }
}