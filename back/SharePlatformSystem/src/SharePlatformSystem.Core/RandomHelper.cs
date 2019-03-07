using SharePlatformSystem.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem
{
    /// <summary>
    ///ʹ�á�Random����Ŀ�ݷ�ʽ��
    ///���ṩ��һЩ���õķ�����
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// ����ָ����Χ�ڵ��������
        /// </summary>
        /// <param name="minValue">���ص�������İ������ޡ�</param>
        /// <param name="maxValue">���ص�������Ķ�ռ���ޡ�MaxValue������ڻ����MinValue��</param>
        /// <returns>
        ///���ڻ����minvalueС��maxvalue��32λ�з���������
        ///������ֵ�ķ�Χ����minvalue��������maxvalue��
        ///���minvalue����maxvalue���򷵻�minvalue��
        /// </returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// ����С��ָ�����ֵ�ķǸ��������
        /// </summary>
        /// <param name="maxValue">Ҫ���ɵ�������Ķ�ռ�Ͻ硣MaxValue������ڻ�����㡣</param>
        /// <returns>
        ///���ڻ��������С��maxvalue��32λ�з���������
        ///Ҳ����˵������ֵ�ķ�Χͨ�������㣬��������MaxValue��
        ///���ǣ����maxvalue�����㣬�򷵻�maxvalue.mum��
        /// </returns>
        public static int GetRandom(int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(maxValue);
            }
        }

        /// <summary>
        /// ���طǸ��������
        /// </summary>
        /// <returns>���ڻ��������С�ڡ�int.maxvalue����32λ�з���������</returns>
        public static int GetRandom()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }

        /// <summary>
        ///��ȡ������������ֵ��
        /// </summary>
        /// <typeparam name="T">���������</typeparam>
        /// <param name="objs">Ҫ���ѡ��Ķ����б�</param>
        public static T GetRandomOf<T>(params T[] objs)
        {
            if (objs.IsNullOrEmpty())
            {
                throw new ArgumentException("obj����Ϊ�գ�", "objs");
            }

            return objs[GetRandom(0, objs.Length)];
        }

        /// <summary>
        ///�Ӹ����Ŀ�ö������������б�
        /// </summary>
        /// <typeparam name="T">�б��е���Ŀ����</typeparam>
        /// <param name="items">items</param>
        public static List<T> GenerateRandomizedList<T>(IEnumerable<T> items)
        {
            var currentList = new List<T>(items);
            var randomList = new List<T>();

            while (currentList.Any())
            {
                var randomIndex = RandomHelper.GetRandom(0, currentList.Count);
                randomList.Add(currentList[randomIndex]);
                currentList.RemoveAt(randomIndex);
            }

            return randomList;
        }
    }
}
