using System.Globalization;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Core.Localization.Sources;
using SharePlatformSystem.Core.ObjectMapping;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Events.Bus;
using SharePlatformSystem.Runtime.Session;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in SharePlatform system.
    /// </summary>
    public abstract class SharePlatformController : Controller,ITransientDependency
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public ISharePlatformSession SharePlatformSession { get; set; }

        /// <summary>
        /// Gets the event bus.
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }


        /// <summary>
        /// Reference to the object to object mapper.
        /// </summary>
        public IObjectMapper ObjectMapper { get; set; }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
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
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
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

        //public IAlertManager AlertManager { get; set; }

        //public AlertList Alerts => AlertManager.Alerts;

        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SharePlatformController()
        {
            SharePlatformSession = NullSharePlatformSession.Instance;
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            EventBus = NullEventBus.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}