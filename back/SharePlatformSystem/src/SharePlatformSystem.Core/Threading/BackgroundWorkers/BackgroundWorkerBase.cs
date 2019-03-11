using System.Globalization;
using Castle.Core.Logging;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Localization.Sources;
using SharePlatformSystem.Domain.Uow;

namespace SharePlatformSystem.Threading.BackgroundWorkers
{
    /// <summary>
    /// 可用于实现的基类<see cref=“ibackgroundworker”/>。
    /// </summary>
    public abstract class BackgroundWorkerBase : RunnableBase, IBackgroundWorker
    {
        /// <summary>
        /// 对设置管理器的引用。
        /// </summary>
        public ISettingManager SettingManager { protected get; set; }

        /// <summary>
        /// 参考<see cref=“iunitofWorkManager”/>。
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new SharePlatformException("必须设置UnitOfWorkManager才能使用它。");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        ///获取当前工作单位。
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// 引用本地化管理器。
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        ///获取/设置此应用程序服务中使用的本地化源的名称。
        /// 必须设置它才能使用<see cref=“l（string）”/>和<see cref=“l（string，cultureinfo）”/>方法。
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

        /// <summary>
        ///引用记录器以写入日志。
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// 构造器.
        /// </summary>
        protected BackgroundWorkerBase()
        {
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public override void Start()
        {
            base.Start();
            Logger.Debug("Start background worker: " + ToString());
        }

        public override void Stop()
        {
            base.Stop();
            Logger.Debug("Stop background worker: " + ToString());
        }

        public override void WaitToStop()
        {
            base.WaitToStop();
            Logger.Debug("WaitToStop background worker: " + ToString());
        }

        /// <summary>
        /// 获取给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地化字符串</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">culture信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地化字符串</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        ///获取给定项名称和指定区域性信息的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">culture信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地化字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// 获取具有格式字符串的给定项名称和当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">culture信息</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>本地化字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        public override string ToString()
        {
            return GetType().FullName;
        }
    }
}