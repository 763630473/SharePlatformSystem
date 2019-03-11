using Castle.DynamicProxy;

namespace SharePlatformSystem.Core.Reflection
{
    public static class ProxyHelper
    {
        /// <summary>
        /// 如果这是代理对象，则返回动态代理目标对象，否则返回给定对象。
        /// </summary>
        public static object UnProxy(object obj)
        {
            return ProxyUtil.GetUnproxiedInstance(obj);
        }
    }
}
