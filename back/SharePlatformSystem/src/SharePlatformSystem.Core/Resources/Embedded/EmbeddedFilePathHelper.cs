using System;
using System.Text;

namespace SharePlatformSystem.Core.Resources.Embedded
{
    public static class EmbeddedResourcePathHelper
    {
        public static string NormalizePath(string fullPath)
        {
            return fullPath?.Replace("/", ".").TrimStart('.');
        }

        public static string EncodeAsResourcesPath(string subPath)
        {
            var builder = new StringBuilder(subPath.Length);

            // 子路径是否包含目录部分-如果是，我们需要对其进行编码。
            var indexOfLastSeperator = subPath.LastIndexOf('/');
            if (indexOfLastSeperator != -1)
            {
                // has directory portion to encode.
                for (int i = 0; i <= indexOfLastSeperator; i++)
                {
                    var currentChar = subPath[i];

                    if (currentChar == '/')
                    {
                        if (i != 0) // 省略起始斜线（/），将其他任何斜线编码为点。
                        {
                            builder.Append('.');
                        }
                        continue;
                    }

                    if (currentChar == '-')
                    {
                        builder.Append('_');
                        continue;
                    }

                    builder.Append(currentChar);
                }
            }

            // 现在附加（并根据需要进行编码）文件名部分
            if (subPath.Length > indexOfLastSeperator + 1)
            {
                // 有要编码的文件名
                for (int c = indexOfLastSeperator + 1; c < subPath.Length; c++)
                {
                    var currentChar = subPath[c];
                    // 不需要对文件名进行编码-所以只需追加
                    builder.Append(currentChar);
                }
            }

            return builder.ToString();
        }
    }
}