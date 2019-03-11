using System.Collections.Generic;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    ///继承此类以定义模块/应用程序的设置。
    /// </summary>
    public abstract class SettingProvider : ITransientDependency
    {
        /// <summary>
        /// 获取此提供程序提供的所有设置定义。
        /// </summary>
        /// <returns>设置列表</returns>
        public abstract IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context);
    }
}