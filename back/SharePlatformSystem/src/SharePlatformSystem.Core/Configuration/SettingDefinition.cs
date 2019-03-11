using System;
using SharePlatformSystem.Core.Localization;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    ///定义一个设置。
    ///设置用于配置和更改应用程序的行为。
    /// </summary>
    public class SettingDefinition
    {
        /// <summary>
        /// 设置的唯一名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 设置的显示名称。
        ///可用于向用户显示设置。
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// 此设置的简要说明。
        /// </summary>
        public ILocalizableString Description { get; set; }

        /// <summary>
        ///此设置的范围。
        ///默认值：“settingscopes.application”。
        /// </summary>
        public SettingScopes Scopes { get; set; }

        /// <summary>
        ///此设置是从父作用域继承的吗？
        ///默认值：真。
        /// </summary>
        public bool IsInherited { get; set; }

        /// <summary>
        ///获取/设置此设置的组。
        /// </summary>
        public SettingDefinitionGroup Group { get; set; }

        /// <summary>
        /// 设置的默认值。
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 客户能看到这个设置和它的值吗？
        ///某些设置对客户端可见（如电子邮件服务器密码）可能很危险。
        ///默认值：false。
        /// </summary>
        [Obsolete("Use ClientVisibilityProvider instead.")]
        public bool IsVisibleToClients { get; set; }

        /// <summary>
        ///设置的客户端可见性定义。
        /// </summary>
        public ISettingClientVisibilityProvider ClientVisibilityProvider { get; set; }

        /// <summary>
        ///可用于存储与此设置相关的自定义对象。
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// 创建一个新的“settingdefinition”对象。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="defaultValue">设置的默认值</param>
        /// <param name="displayName">权限的显示名称</param>
        /// <param name="group">此设置的组</param>
        /// <param name="description">此设置的简要说明</param>
        /// <param name="scopes">此设置的范围。默认值：“settingscopes.application”。</param>
        /// <param name="isVisibleToClients">客户能看到这个设置和它的值吗？默认值：假</param>
        /// <param name="isInherited">此设置是从父作用域继承的吗？默认：True。</param>
        /// <param name="customData">可用于存储与此设置相关的自定义对象</param>
        /// <param name="clientVisibilityProvider">设置的客户端可见性定义。默认：不可见</param>
        public SettingDefinition(
            string name,
            string defaultValue,
            ILocalizableString displayName = null,
            SettingDefinitionGroup group = null,
            ILocalizableString description = null,
            SettingScopes scopes = SettingScopes.Application,
            bool isVisibleToClients = false,
            bool isInherited = true,
            object customData = null,
            ISettingClientVisibilityProvider clientVisibilityProvider = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            DefaultValue = defaultValue;
            DisplayName = displayName;
            Group = @group;
            Description = description;
            Scopes = scopes;
            IsVisibleToClients = isVisibleToClients;
            IsInherited = isInherited;
            CustomData = customData;

            ClientVisibilityProvider = new HiddenSettingClientVisibilityProvider();

            if (isVisibleToClients)
            {
                ClientVisibilityProvider = new VisibleSettingClientVisibilityProvider();
            }
            else if (clientVisibilityProvider != null)
            {
                ClientVisibilityProvider = clientVisibilityProvider;
            }
        }
    }
}
