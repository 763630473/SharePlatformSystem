using System.Globalization;
using Castle.Core.Logging;
using Microsoft.AspNetCore.SignalR;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Localization.Sources;
using SharePlatformSystem.Core.ObjectMapping;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Runtime.Session;

namespace SharePlatform.AspNetCore.SignalR.Hubs
{
    public abstract class SharePlatformHubBase : Hub
    {
        public ILogger Logger { get; set; }

        public ISharePlatformSession SharePlatformSession { get; set; }

        public IIocResolver IocResolver { get; set; }

        public IObjectMapper ObjectMapper { get; set; }

        public ISettingManager SettingManager { get; set; }

        public ILocalizationManager LocalizationManager { get; set; }

        /// <summary>
        /// 获取/设置此应用程序服务中使用的本地化源的名称。
        ///必须设置它才能使用<see cref=“l（string）”/>和<see cref=“l（string，cultureinfo）”/>方法。
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        ///获取本地化源。
        ///如果设置了<see cref=“localizationsourcename”/>，则此选项有效。
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new SharePlatformException("必须在之前设置本地化源名称，才能获取本地化源");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        private ILocalizationSource _localizationSource;

        protected bool Disposed { get; private set; }

        protected SharePlatformHubBase()
        {
            Logger = NullLogger.Instance;
            ObjectMapper = NullObjectMapper.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        ///获取给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <returns>本地字符串</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// 获取给定项名称和指定区域性信息的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        ///获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">culture 信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                Disposed = true;
                IocResolver?.Release(this);
            }
        }
    }
}
