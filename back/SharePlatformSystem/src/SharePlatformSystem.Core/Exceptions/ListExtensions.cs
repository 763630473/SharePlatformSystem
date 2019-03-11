using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    ///<see cref=“ilist t”/>的扩展方法。
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        ///通过拓扑排序对列表进行排序，这考虑到它们的依赖性。
        /// </summary>
        /// <typeparam name="T">值成员的类型。</typeparam>
        /// <param name="source">要排序的对象列表</param>
        /// <param name="getDependencies">函数来解析依赖项</param>
        /// <returns></returns>
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {

            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">值成员的类型。</typeparam>
        /// <param name="item">要解决的项目</param>
        /// <param name="getDependencies">函数来解析依赖项</param>
        /// <param name="source">要排序的对象列表</param>
        /// <param name="visited">函数来解析依赖项</param>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("找到循环依赖项！项目： " + item);
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
