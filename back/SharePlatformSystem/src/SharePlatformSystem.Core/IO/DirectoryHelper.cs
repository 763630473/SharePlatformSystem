using System.IO;

namespace SharePlatformSystem.Core.IO
{
    /// <summary>
    /// 用于目录操作的帮助程序类。
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// 如果新目录不存在，则创建新目录。
        /// </summary>
        /// <param name="directory">Directory to create</param>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}