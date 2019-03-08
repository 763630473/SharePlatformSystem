using SharePlatformSystem.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem
{
    /// <summary>
    ///使用“Random”类的快捷方式。
    ///还提供了一些有用的方法。
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// 返回指定范围内的随机数。
        /// </summary>
        /// <param name="minValue">返回的随机数的包含下限。</param>
        /// <param name="maxValue">返回的随机数的独占上限。MaxValue必须大于或等于MinValue。</param>
        /// <returns>
        ///大于或等于minvalue小于maxvalue的32位有符号整数；
        ///即返回值的范围包括minvalue，而不是maxvalue。
        ///如果minvalue等于maxvalue，则返回minvalue。
        /// </returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// 返回小于指定最大值的非负随机数。
        /// </summary>
        /// <param name="maxValue">要生成的随机数的独占上界。MaxValue必须大于或等于零。</param>
        /// <returns>
        ///大于或等于零且小于maxvalue的32位有符号整数；
        ///也就是说，返回值的范围通常包括零，但不包括MaxValue。
        ///但是，如果maxvalue等于零，则返回maxvalue.mum。
        /// </returns>
        public static int GetRandom(int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(maxValue);
            }
        }

        /// <summary>
        /// 返回非负随机数。
        /// </summary>
        /// <returns>大于或等于零且小于“int.maxvalue”的32位有符号整数。</returns>
        public static int GetRandom()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }

        /// <summary>
        ///获取给定对象的随机值。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="objs">要随机选择的对象列表</param>
        public static T GetRandomOf<T>(params T[] objs)
        {
            if (objs.IsNullOrEmpty())
            {
                throw new ArgumentException("obj不能为空！", "objs");
            }

            return objs[GetRandom(0, objs.Length)];
        }

        /// <summary>
        ///从给定的可枚举项生成随机列表。
        /// </summary>
        /// <typeparam name="T">列表中的项目类型</typeparam>
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
