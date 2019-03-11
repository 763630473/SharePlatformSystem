using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace SharePlatformSystem.Core.Resources.Embedded
{
    /// <summary>
    /// 存储嵌入资源的所需信息。
    /// </summary>
    public class EmbeddedResourceItem
    {
        /// <summary>
        /// 文件名，包括扩展名。
        /// </summary>
        public string FileName { get; }

        [CanBeNull]
        public string FileExtension { get; }

        /// <summary>
        /// 资源文件的内容。
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// 包含资源的程序集。
        /// </summary>
        public Assembly Assembly { get; set; }

        public DateTime LastModifiedUtc { get; }

        internal EmbeddedResourceItem(string fileName, byte[] content, Assembly assembly)
        {
            FileName = fileName;
            Content = content;
            Assembly = assembly;
            FileExtension = CalculateFileExtension(FileName);
            LastModifiedUtc = Assembly.Location != null
                ? new FileInfo(Assembly.Location).LastWriteTimeUtc
                : DateTime.UtcNow;
        }

        private static string CalculateFileExtension(string fileName)
        {
            if (!fileName.Contains("."))
            {
                return null;
            }

            return fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
        }
    }
}