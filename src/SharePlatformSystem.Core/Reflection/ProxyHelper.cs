﻿using Castle.DynamicProxy;

namespace SharePlatformSystem.Core
{
    public static class ProxyHelper
    {
        /// <summary>
        /// Returns dynamic proxy target object if this is a proxied object, otherwise returns the given object. 
        /// </summary>
        public static object UnProxy(object obj)
        {
            return ProxyUtil.GetUnproxiedInstance(obj);
        }
    }
}
