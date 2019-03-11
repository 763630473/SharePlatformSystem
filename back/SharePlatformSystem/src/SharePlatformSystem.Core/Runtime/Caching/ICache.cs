using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    /// ������԰����洢�ͻ�ȡ��Ŀ�Ļ��档
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// �����Ψһ���ơ�
        /// </summary>
        string Name { get; }

        /// <summary>
        ///�������Ĭ�ϻ�������ʱ�䡣
        ///Ĭ��ֵ��60���ӣ�1Сʱ����
        ///����ͨ�����ø��ġ�
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        ///�������Ĭ�Ͼ��Թ���ʱ�䡣
        ///Ĭ��ֵ���գ�δʹ�ã���
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// <summary>
        ///�ӻ����л�ȡ�
        ///�˷������ػ����ṩ����ʧ�ܣ�����¼���ǣ���
        ///��������ṩ����ʧ�ܣ���ʹ�ù���������ȡ����
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>������</returns>
        object Get(string key, Func<string, object> factory);

        /// <summary>
        ///�ӻ����л�ȡ�
        ///�˷������ػ����ṩ����ʧ�ܣ�����¼���ǣ���
        ///��������ṩ����ʧ�ܣ���ʹ�ù���������ȡ����
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>������</returns>
        object[] Get(string[] keys, Func<string, object> factory);

        /// <summary>
        ///�ӻ����л�ȡ�
        ///�˷������ػ����ṩ����ʧ�ܣ�����¼���ǣ���
        ///��������ṩ����ʧ�ܣ���ʹ�ù���������ȡ����
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>������</returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);

        /// <summary>
        ///�ӻ����л�ȡ�
        ///�˷������ػ����ṩ����ʧ�ܣ�����¼���ǣ���
        ///��������ṩ����ʧ�ܣ���ʹ�ù���������ȡ����
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>������</returns>
        Task<object[]> GetAsync(string[] keys, Func<string, Task<object>> factory);

        /// <summary>
        /// �ӻ����л�ȡ����δ�ҵ�����Ϊ�ա�
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>���������Ҳ�����Ϊ��</returns>
        object GetOrDefault(string key);

        /// <summary>
        /// �ӻ����ȡ�����ÿ��δ�ҵ��ļ������᷵��һ����ֵ��
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>������</returns>
        object[] GetOrDefault(string[] keys);

        /// <summary>
        /// �ӻ����л�ȡ����δ�ҵ�����Ϊ�ա�
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>���������Ҳ�����Ϊ��</returns>
        Task<object> GetOrDefaultAsync(string key);

        /// <summary>
        /// �ӻ����ȡ�����ÿ��δ�ҵ��ļ������᷵��һ����ֵ��
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>������</returns>
        Task<object[]> GetOrDefaultAsync(string[] keys);

        /// <summary>
        ///�ü�����/��д�����е��
        ///���ʹ��һ������ʱ�䣨<paramref name=��slidingExpireTime��/>��<paramref name=��absoluteExpireTime��/>����
        ///���û��ָ������
        ///<see cref=��defaultAbsoluteExpireTime��/>�����Ϊ�գ���ʹ������Othewise��<see cref=��defaultslidingExpireTime��/>
        ///����ʹ�á�
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        ///���Ա���/��д�����е��
        ///���ʹ��һ������ʱ�䣨<paramref name=��slidingExpireTime��/>��<paramref name=��absoluteExpireTime��/>����
        ///���û��ָ������
        ///<see cref=��defaultAbsoluteExpireTime��/>�����Ϊ�գ���ʹ������Othewise��<see cref=��defaultslidingExpireTime��/>
        ///����ʹ�á�
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        void Set(KeyValuePair<string, object>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        ///�ü�����/��д�����е��
        ///���ʹ��һ������ʱ�䣨<paramref name=��slidingExpireTime��/>��<paramref name=��absoluteExpireTime��/>����
        ///���û��ָ������
        ///<see cref=��defaultAbsoluteExpireTime��/>�����Ϊ�գ���ʹ������Othewise��<see cref=��defaultslidingExpireTime��/>

        ///����ʹ�á�
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// ���Ա���/��д�����е��
        ///���ʹ��һ������ʱ�䣨<paramref name=��slidingExpireTime��/>��<paramref name=��absoluteExpireTime��/>����
        /// ���û��ָ������
        ///<see cref=��defaultAbsoluteExpireTime��/>�����Ϊ�գ���ʹ������Othewise��<see cref=��defaultslidingExpireTime��/>
        ///����ʹ�á�
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        Task SetAsync(KeyValuePair<string, object>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        ///��������ļ�ɾ���������������в����ڸ����ļ�����ִ���κβ�������
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(string key);

        /// <summary>
        /// ����ɾ�������
        /// </summary>
        /// <param name="keys">Keys</param>
        void Remove(string[] keys);

        /// <summary>
        /// ��������ļ�ɾ���������������в����ڸ����ļ�����ִ���κβ�������
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(string key);

        /// <summary>
        ///�����Ƴ������
        /// </summary>
        /// <param name="keys">Keys</param>
        Task RemoveAsync(string[] keys);

        /// <summary>
        ///����˻����е�������Ŀ��
        /// </summary>
        void Clear();

        /// <summary>
        ///����˻����е������
        /// </summary>
        Task ClearAsync();
    }
}
