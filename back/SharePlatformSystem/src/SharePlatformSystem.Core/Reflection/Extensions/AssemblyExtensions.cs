using System.IO;
using System.Reflection;

namespace SharePlatformSystem.Core.Reflection.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取给定程序集的目录路径，如果找不到，则返回空值。
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static string GetDirectoryPathOrNull(this Assembly assembly)
        {
            var location = assembly.Location;
            if (location == null)
            {
                return null;
            }

            var directory = new FileInfo(location).Directory;
            if (directory == null)
            {
                return null;
            }

            return directory.FullName;
        }
    }
}
