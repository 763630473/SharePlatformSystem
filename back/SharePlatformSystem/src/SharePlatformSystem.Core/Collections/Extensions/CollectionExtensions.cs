using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Collections.Extensions
{
    /// <summary>
    /// ���ϵ���չ������
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///�������ļ��϶����Ƿ�Ϊ�ջ�û���
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// ���������û���������ӵ������С�
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Ҫ������ӵ���</param>
        /// <typeparam name="T">collection����Ŀ������</typeparam>
        /// <returns>�������򷵻�true�����򷵻�false��</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}