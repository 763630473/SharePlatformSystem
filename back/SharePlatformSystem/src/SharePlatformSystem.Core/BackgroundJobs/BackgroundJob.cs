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
    /// ������ʵ�ֵĻ��ࡣ
    /// </summary>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        /// <summary>
        /// �����ù����������á�
        /// </summary>
        public ISettingManager SettingManager { protected get; set; }

        /// <summary>
        ///"IUnitOfWorkManager"������.
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
        /// ��ȡ��ǰ������λ��
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// ���ñ��ػ���������
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// ��ȡ/���ô�Ӧ�ó��������ʹ�õı��ػ�Դ�����ơ�
        ///��������������ʹ�á�l��string�����͡�l��string��cultureinfo����������
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// ��ȡ���ػ�Դ��
        /// ��������ˡ�localizationsourcename�������ѡ����Ч��
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
        ///���ü�¼����д����־��
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// ������.
        /// </summary>
        protected BackgroundJob()
        {
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public abstract void Execute(TArgs args);

        /// <summary>
        ///��ȡ���������ƺ͵�ǰ���Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ֵ</param>
        /// <returns>���ػ��ַ���</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// ��ȡ���и�ʽ�ַ����ĸ��������ƺ͵�ǰ���Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ֵ</param>
        /// <param name="args">���ò�����ʽ</param>
        /// <returns>���ػ��ַ���</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// ��ȡ���������ƺ�ָ����������Ϣ�ı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ֵ</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <returns>���ػ��ַ���</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// ��ȡ���и�ʽ�ַ����ĸ��������ƺ͵�ǰ���Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ֵ</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <param name="args">���ò�����ʽ</param>
        /// <returns>���ػ��ַ���</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}