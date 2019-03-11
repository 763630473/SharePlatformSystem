using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Localization.Sources;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Views
{
    /// <summary>
    /// SharePlatform系统中所有视图的基类。
    /// </summary>
    /// <typeparam name="TModel">视图模型的类型</typeparam>
    public abstract class SharePlatformRazorPage<TModel> : RazorPage<TModel>
    {
        /// <summary>
        /// 获取应用程序的根路径。
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                var appPath = Context.Request.PathBase.Value;
                if (appPath == null)
                {
                    return "/";
                }

                appPath = appPath.EnsureEndsWith('/');

                return appPath;
            }
        }

        /// <summary>
        /// 引用本地化管理器。
        /// </summary>
        [RazorInject]
        public ILocalizationManager LocalizationManager { get; set; }

        /// <summary>
        /// 对设置管理器的引用。
        /// </summary>
        [RazorInject]
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        ///获取/设置此控制器中使用的本地化源的名称。
        ///必须设置它才能使用<see cref=“l（string）”/>和<see cref=“l（string，cultureinfo）”/>方法。
        /// </summary>
        protected string LocalizationSourceName
        {
            get { return _localizationSource.Name; }
            set { _localizationSource = LocalizationHelper.GetSource(value); }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SharePlatformRazorPage()
        {
            _localizationSource = NullLocalizationSource.Instance;
        }

        /// <summary>
        /// 获取给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <returns>本地字符串</returns>
        protected virtual string L(string name)
        {
            return _localizationSource.GetString(name);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected virtual string L(string name, params object[] args)
        {
            return _localizationSource.GetString(name, args);
        }

        /// <summary>
        /// 获取给定项名称和指定区域性信息的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">culture 信息</param>
        /// <returns>本地字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return _localizationSource.GetString(name, culture);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">culture 信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return _localizationSource.GetString(name, culture, args);
        }

        /// <summary>
        /// 从给定源获取给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="sourceName">源名称</param>
        /// <param name="name">键名</param>
        /// <returns>本地字符串</returns>
        protected virtual string Ls(string sourceName, string name)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// 从给定源获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="sourceName">源名称</param>
        /// <param name="name">键名</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected virtual string Ls(string sourceName, string name, params object[] args)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name, args);
        }

        /// <summary>
        ///从给定源获取给定项名称和指定区域性信息的本地化字符串。
        /// </summary>
        /// <param name="sourceName">源名称</param>
        /// <param name="name">键名</param>
        /// <param name="culture">culture 信息</param>
        /// <returns>本地字符串</returns>
        protected virtual string Ls(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name, culture);
        }

        /// <summary>
        /// 从给定源获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="sourceName">源名称</param>
        /// <param name="name">键名</param>
        /// <param name="culture">culture 信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地字符串</returns>
        protected virtual string Ls(string sourceName, string name, CultureInfo culture, params object[] args)
        {
            return LocalizationManager.GetSource(sourceName).GetString(name, culture, args);
        }      

    }
}
