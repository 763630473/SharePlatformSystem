using System.IO;

namespace SharePlatformSystem.Core.IO
{
    /// <summary>
    /// ����Ŀ¼�����İ��������ࡣ
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// �����Ŀ¼�����ڣ��򴴽���Ŀ¼��
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