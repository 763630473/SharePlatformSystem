using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Domain.Uow;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Runtime.Session;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// ���й�����Ԫ��Ļ�����
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public string Id { get; }

        [DoNotWire]
        public IUnitOfWork Outer { get; set; }

        public event EventHandler Completed;

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        public event EventHandler Disposed;

        public UnitOfWorkOptions Options { get; private set; }

        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }
        private readonly List<DataFilterConfiguration> _filters;

        public Dictionary<string, object> Items { get; set; }

        /// <summary>
        /// ��ȡĬ��UOWѡ�
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; }

        /// <summary>
        ///��ȡ�����ַ�����������
        /// </summary>
        protected IConnectionStringResolver ConnectionStringResolver { get; }

        /// <summary>
        /// ��ȡһ��ֵ����ֵָʾ�˹�����Ԫ�Ƿ����ͷš�
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// �Ե�ǰSharePlatform�Ự�����á�
        /// </summary>
        public ISharePlatformSession SharePlatformSystemSession { protected get; set; }

        protected IUnitOfWorkFilterExecuter FilterExecuter { get; }

        /// <summary>
        /// ֮ǰ�Ƿ�����ˡ�Begin��������
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// ��ǰ���ù���complete��������
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// �˹�����Ԫ�Ƿ��ѳɹ���ɣ�
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// ����˹�����Ԫʧ�ܣ��������쳣��
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// ������.
        /// </summary>
        protected UnitOfWorkBase(
            IConnectionStringResolver connectionStringResolver, 
            IUnitOfWorkDefaultOptions defaultOptions,
            IUnitOfWorkFilterExecuter filterExecuter)
        {
            FilterExecuter = filterExecuter;
            DefaultOptions = defaultOptions;
            ConnectionStringResolver = connectionStringResolver;

            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();

            SharePlatformSystemSession = NullSharePlatformSession.Instance;
            Items = new Dictionary<string, object>();
        }

        public void Begin(UnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));

            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

            SetFilters(options.FilterOverrides);

            BeginUow();
        }

        public abstract void SaveChanges();

        public abstract Task SaveChangesAsync();

        public IDisposable DisableFilter(params string[] filterNames)
        {
            //TODO: ���������Ƿ���ڣ�

            var disabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], false);
                }
            }

            disabledFilters.ForEach(ApplyDisableFilter);

            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        }

        /// <inheritdoc/>
        public IDisposable EnableFilter(params string[] filterNames)
        {
            //TODO: ���������Ƿ���ڣ�

            var enabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], true);
                }
            }

            enabledFilters.ForEach(ApplyEnableFilter);

            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }

        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            var filterIndex = GetFilterIndex(filterName);

            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

            //�洢��ֵ
            object oldValue = null;
            var hasOldValue = newfilter.FilterParameters.ContainsKey(parameterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[parameterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //�洢��ֵ
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }
        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        public void Dispose()
        {
            if (!_isBeginCalledBefore || IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        /// <summary>
        ///������������ʵ��������UOW��
        /// </summary>
        protected virtual void BeginUow()
        {
            
        }

        /// <summary>
        /// Ӧ����������ʵ�������UOW��
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Ӧ����������ʵ�������UOW��
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        ///Ӧ����������ʵ�����ͷ�UOW��
        /// </summary>
        protected abstract void DisposeUow();

        protected virtual void ApplyDisableFilter(string filterName)
        {
            FilterExecuter.ApplyDisableFilter(this, filterName);
        }

        protected virtual void ApplyEnableFilter(string filterName)
        {
            FilterExecuter.ApplyEnableFilter(this, filterName);
        }

        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            FilterExecuter.ApplyFilterParameterValue(this, filterName, parameterName, value);
        }

        protected virtual string ResolveConnectionString(ConnectionStringResolveArgs args)
        {
            return ConnectionStringResolver.GetNameOrConnectionString(args);
        }

        /// <summary>
        /// �����Դ���������ɡ��¼���
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        ///�����Դ�����ʧ�ܡ��¼���
        /// </summary>
        /// <param name="exception">����ʧ�ܵ��쳣</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// �����Դ��������ͷš��¼���
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new SharePlatformException("�������ǰ�Ϳ�ʼ�ˡ����ܶ�ε���Start������");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new SharePlatformException("֮ǰ������ɣ�");
            }

            _isCompleteCalledBefore = true;
        }

        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }
        
        }

        private void ChangeFilterIsEnabledIfNotOverrided(List<DataFilterConfiguration> filterOverrides, string filterName, bool isEnabled)
        {
            if (filterOverrides.Any(f => f.FilterName == filterName))
            {
                return;
            }

            var index = _filters.FindIndex(f => f.FilterName == filterName);
            if (index < 0)
            {
                return;
            }

            if (_filters[index].IsEnabled == isEnabled)
            {
                return;
            }

            _filters[index] = new DataFilterConfiguration(filterName, isEnabled);
        }

        private DataFilterConfiguration GetFilter(string filterName)
        {
            var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new SharePlatformException("δ֪��ɸѡ�����ƣ�" + filterName + ". ȷ����ɸѡ����ǰ��ע�ᡣ");
            }

            return filter;
        }

        private int GetFilterIndex(string filterName)
        {
            var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new SharePlatformException("δ֪��ɸѡ�����ƣ�" + filterName + ".  ȷ����ɸѡ����ǰ��ע�ᡣ.");
            }

            return filterIndex;
        }

        public override string ToString()
        {
            return $"[UnitOfWork {Id}]";
        }
    }
}