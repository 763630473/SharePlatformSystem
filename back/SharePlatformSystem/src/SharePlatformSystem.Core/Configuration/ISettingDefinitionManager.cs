using System.Collections.Generic;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 定义设置定义管理器。
    /// </summary>
    public interface ISettingDefinitionManager
    {
        /// <summary>
        /// 获取具有给定唯一名称的“settingdefinition”对象。
        /// 如果找不到设置，则引发异常。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>“settingdefinition”对象。</returns>
        SettingDefinition GetSettingDefinition(string name);

        /// <summary>
        ///获取所有设置定义的列表。
        /// </summary>
        /// <returns>All settings.</returns>
        IReadOnlyList<SettingDefinition> GetAllSettingDefinitions();
    }
}
