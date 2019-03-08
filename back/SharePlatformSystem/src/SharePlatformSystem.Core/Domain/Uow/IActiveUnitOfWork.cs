using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///�˽ӿ�����ʹ�û������Ԫ��
    ///�޷�ע��˽ӿڡ�
    ///��Ϊʹ�á�iunitofworkmanager����
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        ///��UOW�ɹ����ʱ�������¼���
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        ///��UOWʧ��ʱ�������¼���
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// �ͷŴ�UOWʱ�������¼���
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// ��ȡ�˹�����Ԫ�Ƿ��������Եġ�
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        ///��ȡ�˹�����Ԫ������ɸѡ�����á�
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        ///������UnitOfWork�Ͻ����Զ���������ֵ�
        /// </summary>
        Dictionary<string, object> Items { get; set; }

        /// <summary>
        /// ��UOW�Ƿ��Ѵ���
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        ///�ڴ˹�����Ԫ�б��浽����Ϊֹ�����и��ġ�
        ///���Ե��ô˷���������ҪʱӦ�ø��ġ�
        ///��ע�⣬����˹�����Ԫ�������Եģ����������ع����򱣴�ĸ���Ҳ���ع���
        ///һ�㲻��Ҫ��ʽ������������ģ�
        ///��Ϊ���и��Ķ����ڹ�����Ԫ����ʱ�Զ�����ġ�
        /// </summary>
        void SaveChanges();

        /// <summary>
        ///�ڴ˹�����Ԫ�б��浽����Ϊֹ�����и��ġ�
        ///���Ե��ô˷���������ҪʱӦ�ø��ġ�
        ///��ע�⣬����˹�����Ԫ�������Եģ����������ع����򱣴�ĸ���Ҳ���ع���
        ///һ�㲻��Ҫ��ʽ������������ģ�
        ///��Ϊ���и��Ķ����ڹ�����Ԫ����ʱ�Զ�����ġ�
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        ///����һ����������ɸѡ����
        ///����������ѱ����ã���ִ���κβ�����
        ///��using�����ʹ�ô˷�������Ҫʱ��������ɸѡ����
        /// </summary>
        /// <param name="filterNames">һ������ɸѡ�����ơ������ڱ�׼ɸѡ����SharePlatformDataFilters��</param>
        /// <returns>A <see cref="IDisposable"/>�ֱ��ָ�����Ч����</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        ///����һ����������ɸѡ����
        ///�������õ�ɸѡ����ִ���κβ�����
        ///�����Ҫ��������using�����ʹ�ô˷������½���ɸѡ����
        /// </summary>
        /// <param name="filterNames">һ������ɸѡ�����ơ������ڱ�׼ɸѡ����SharePlatformDataFilters��</param>
        /// <returns>һ����idisposable��������ָ�����Ч����</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        ///����Ƿ�������ɸѡ����
        /// </summary>
        /// <param name="filterName">ɸѡ�������ơ������ڱ�׼ɸѡ����SharePlatformDataFilters��</param>
        bool IsFilterEnabled(string filterName);

        /// <summary>
        ///���ã����ǣ�������������ֵ��
        /// </summary>
        /// <param name="filterName">ɸѡ��������</param>
        /// <param name="parameterName">����������</param>
        /// <param name="value">Ҫ���õĲ�����ֵ</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);
    }
}