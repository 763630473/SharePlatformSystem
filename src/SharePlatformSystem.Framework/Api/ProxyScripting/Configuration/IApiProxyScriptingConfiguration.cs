﻿using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Framework.Api.ProxyScripting.Configuration
{
    public interface IApiProxyScriptingConfiguration
    {
        /// <summary>
        /// Used to add/replace proxy script generators. 
        /// </summary>
        IDictionary<string, Type> Generators { get; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool RemoveAsyncPostfixOnProxyGeneration { get; set; }
    }
}