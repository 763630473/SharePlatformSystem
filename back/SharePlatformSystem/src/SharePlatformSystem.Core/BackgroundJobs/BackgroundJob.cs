using System.Globalization;
using Castle.Core.Logging;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Localization.Sources;
using SharePlatformSystem.Domain.Uow;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// 可用于实现的基类。
    /// </summary>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        /// <summary>
        /// 对设置管理器的引用。
        /// </summary>
        public ISettingManager SettingManager { protected get; set; }

        /// <summary>
        ///"IUnitOfWorkManager"的引用.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new SharePlatformException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 获取当前工作单位。
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// 引用本地化管理器。
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// 获取/设置此应用程序服务中使用的本地化源的名称。
        ///必须设置它才能使用“l（string）”和“l（string，cultureinfo）”方法。
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// 获取本地化源。
        /// 如果设置了“localizationsourcename”，则此选项有效。
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new SharePlatformException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        ///引用记录器以写入日志。
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// 构造器.
        /// </summary>
        protected BackgroundJob()
        {
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public abstract void Execute(TArgs args);

        /// <summary>
        ///获取给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键值</param>
        /// <returns>本地化字符串</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键值</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地化字符串</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// 获取给定项名称和指定区域性信息的本地化字符串。
        /// </summary>
        /// <param name="name">键值</param>
        /// <param name="culture">文化信息</param>
        /// <returns>本地化字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键值</param>
        /// <param name="culture">文化信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地化字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}