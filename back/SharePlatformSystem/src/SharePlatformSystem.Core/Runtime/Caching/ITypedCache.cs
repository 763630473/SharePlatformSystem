using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    ///�����ͻ���ʽ������Ľӿڡ�
    ///ʹ��<see cref=��cacheextensions.astyped tkey��tValue��/>����
    ///��a<see cref=��icache��/>ת��Ϊ�˽ӿڡ�
    /// </summary>
    /// <typeparam name="TKey">������ļ�����</typeparam>
    /// <typeparam name="TValue">�������ֵ����</typeparam>
    public interface ITypedCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// �����Ψһ���ơ�
        /// </summary>
        string Name { get; }

        /// <summary>
        /// �������Ĭ�ϻ�������ʱ�䡣
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// ��ȡ�ڲ����档
        /// </summary>
        ICache InternalCache { get; }

        /// <summary>
        /// �ӻ����л�ȡ�
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>Cached item</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);

        /// <summary>
        /// �ӻ����ȡ�
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>Cached items</returns>
        TValue[] Get(TKey[] keys, Func<TKey, TValue> factory);

        /// <summary>
        /// �ӻ����л�ȡ�
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>������</returns>
        Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// �ӻ����ȡ�
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">��������ڣ��򴴽�������Ĺ�������</param>
        /// <returns>������</returns>
        Task<TValue[]> GetAsync(TKey[] keys, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// �ӻ����л�ȡ����δ�ҵ�����Ϊ�ա�
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>���������Ҳ�����Ϊ��</returns>
        TValue GetOrDefault(TKey key);

        /// <summary>
        /// �ӻ����ȡ�����ÿ��δ�ҵ��ļ������᷵��һ����ֵ��
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>������</returns>
        TValue[] GetOrDefault(TKey[] keys);

        /// <summary>
        /// �ӻ����л�ȡ����δ�ҵ�����Ϊ�ա�
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>���������Ҳ�����Ϊ��</returns>
        Task<TValue> GetOrDefaultAsync(TKey key);

        /// <summary>
        ///�ӻ����ȡ�����ÿ��δ�ҵ��ļ������᷵��һ����ֵ��
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>������</returns>
        Task<TValue[]> GetOrDefaultAsync(TKey[] keys);

        /// <summary>
        /// ͨ��������/��д�����е��
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// ���Ա���/��д�����е��
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        void Set(KeyValuePair<TKey, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// ͨ��������/��д�����е��
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// ���Ա���/��д�����е��
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Ե���ʱ��</param>
        Task SetAsync(KeyValuePair<TKey, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// ��������ļ�ɾ���������������в����ڸ����ļ�����ִ���κβ�������
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(TKey key);

        /// <summary>
        /// ����ɾ�������
        /// </summary>
        /// <param name="keys">Keys</param>
        void Remove(TKey[] keys);

        /// <summary>
        ///��������ļ�ɾ���������������в����ڸ����ļ�����ִ���κβ�������
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(TKey key);

        /// <summary>
        /// ����ɾ�������
        /// </summary>
        /// <param name="keys">Keys</param>
        Task RemoveAsync(TKey[] keys);

        /// <summary>
        ///����˻����е�������Ŀ��
        /// </summary>
        void Clear();

        /// <summary>
        /// ����˻����е�������Ŀ��
        /// </summary>
        Task ClearAsync();
    }
}