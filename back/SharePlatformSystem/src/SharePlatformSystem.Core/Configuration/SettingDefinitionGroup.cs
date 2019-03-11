using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    ///设置组用于将某些设置分组到一起。
    ///一个组可以是另一个组的子组，也可以有子组。
    /// </summary>
    public class SettingDefinitionGroup
    {
        /// <summary>
        ///设置组的唯一名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///设置的显示名称。
        ///可用于向用户显示设置。
        /// </summary>
        public ILocalizableString DisplayName { get; private set; }

        /// <summary>
        ///获取此组的父级。
        /// </summary>
        public SettingDefinitionGroup Parent { get; private set; }

        /// <summary>
        /// 获取此组的所有子级的列表。
        /// </summary>
        public IReadOnlyList<SettingDefinitionGroup> Children
        {
            get { return _children.ToImmutableList(); }
        }
        private readonly List<SettingDefinitionGroup> _children;

        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        /// <param name="name">设置组的唯一名称</param>
        /// <param name="displayName">设置的显示名称</param>
        public SettingDefinitionGroup(string name, ILocalizableString displayName)
        {
           Check.NotNullOrWhiteSpace(name, nameof(name));

            Name = name;
            DisplayName = displayName;
            _children = new List<SettingDefinitionGroup>();
        }

        /// <summary>
        /// 将“settingDefinitionGroup”添加为此组的子组。
        /// </summary>
        /// <param name="child">要添加的子项</param>
        /// <returns>此子组可以添加更多子组</returns>
        public SettingDefinitionGroup AddChild(SettingDefinitionGroup child)
        {
            if (child.Parent != null)
            {
                throw new SharePlatformException("设置组 " + child.Name + " 已经有一个父级(" + child.Parent.Name + ").");
            }
            return this;
        }
    }
}