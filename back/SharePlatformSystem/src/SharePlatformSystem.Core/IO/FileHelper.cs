using System.IO;

namespace SharePlatformSystem.Core.IO
{
    /// <summary>
    /// 用于文件操作的帮助程序类。
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 检查并删除给定文件（如果存在）。
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
