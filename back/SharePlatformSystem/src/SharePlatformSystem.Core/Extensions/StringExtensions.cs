using Castle.Core.Internal;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SharePlatformSystem.Core.Extensions
{
    /// <summary>
    /// 字符串类的扩展方法。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///如果不以字符结尾，则在给定字符串的结尾添加一个字符。
        /// </summary>
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        /// 如果不以字符结尾，则在给定字符串的结尾添加一个字符。
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 如果不以字符结尾，则在给定字符串的结尾添加一个字符。
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 如果给定字符串的开头不是以字符开头，则向其添加字符。
        /// </summary>
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        ///如果给定字符串的开头不是以字符开头，则向其添加字符。
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// 如果给定字符串的开头不是以字符开头，则向其添加字符。
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// 指示此字符串是空字符串还是System.String.Empty字符串。
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 指示此字符串是空字符串还是仅由空格字符组成。
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 从字符串的开头获取字符串的子字符串。
        /// </summary>
        /// <exception cref="ArgumentNullException">如果<paramref name=“str”/>为空则引发</exception>
        /// <exception cref="ArgumentException">如果字符串的长度大于</exception>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len参数不能大于给定字符串的长度！");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// 将字符串中的行尾转换为<see cref=“environment.newline”/>。
        /// </summary>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// 获取字符串中第n个字符出现的索引。
        /// </summary>
        /// <param name="str">要搜索的源字符串</param>
        /// <param name="c">要搜索的字符<see cref=“str”/></param>
        /// <param name="n">发生的计数</param>
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        ///从给定字符串的末尾删除第一次出现的给定后缀。
        ///排序很重要。如果其中一个修复匹配，则不会测试其他修复。
        /// </summary>
        /// <param name="str">字符串。</param>
        /// <param name="postFixes">一个或多个后缀.</param>
        /// <returns>修改后的字符串或相同的字符串（如果没有任何给定的后缀）</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        ///从给定字符串的开头删除第一个给定前缀。
        ///排序很重要。如果其中一个前缀匹配，则不会测试其他前缀。
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="preFixes">个或多个前缀。</param>
        /// <returns>修改后的字符串或相同的字符串（如果它没有任何给定前缀）</returns>
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (preFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        ///从字符串结尾获取字符串的子字符串。
        /// </summary>
        /// <exception cref="ArgumentNullException">str为空抛异常</exception>
        /// <exception cref="ArgumentException">如果len的长度大于字符串的长度，则抛出</exception>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len参数不能大于给定字符串的长度！");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        ///使用string.split方法按给定分隔符拆分给定的字符串。
        /// </summary>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// 使用string.split方法按给定分隔符拆分给定的字符串。
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        ///使用String.Split方法将给定字符串拆分为<see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        ///使用String.Split方法将给定字符串拆分为 <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// 将pascalcase字符串转换为camelcase字符串。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="invariantCulture">不变量 culture</param>
        /// <returns>驼峰字符串</returns>
        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToLowerInvariant() : str.ToLower();
            }

            return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])) + str.Substring(1);
        }

        /// <summary>
        ///将指定区域性中的pascalcase字符串转换为camelcase字符串。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="culture">提供特定于区域性的大小写规则的对象</param>
        /// <returns>驼峰字符串</returns>
        public static string ToCamelCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLower(culture);
            }

            return char.ToLower(str[0], culture) + str.Substring(1);
        }

        /// <summary>
        ///将给定的pascalcase/camelcase字符串转换为句子（通过按空格拆分单词）。
        ///示例：“thisIssampleSentence”转换为“This is a sample sentence”。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="invariantCulture">不变量culture</param>
        public static string ToSentenceCase(this string str, bool invariantCulture = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(
                str,
                "[a-z][A-Z]",
                m => m.Value[0] + " " + (invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]))
            );
        }

        /// <summary>
        ///将给定的pascalcase/camelcase字符串转换为句子（通过按空格拆分单词）。
        ///示例：“thisIssampleSentence”转换为“This is a sample sentence”。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="culture">提供特定于区域性的大小写规则的对象。</param>
        public static string ToSentenceCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1], culture));
        }

        /// <summary>
        ///将字符串转换为枚举值。
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="value">要转换的字符串</param>
        /// <returns>返回枚举对象</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// 将字符串转换为枚举值。
        /// </summary>
        /// <typeparam name="T">枚举的类型</typeparam>
        /// <param name="value">要转换的字符串</param>
        /// <param name="ignoreCase">忽略案例</param>
        /// <returns>返回枚举对象</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 将camelcase字符串转换为pascalcase字符串。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="invariantCulture">不变 culture</param>
        /// <returns>字符串的Pascalcase</returns>
        public static string ToPascalCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToUpperInvariant(): str.ToUpper();
            }

            return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])) + str.Substring(1);
        }

        /// <summary>
        /// 将指定区域性中的camelcase字符串转换为pascalcase字符串。
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="culture">提供特定于区域性的大小写规则的对象</param>
        /// <returns>字符串的Pascalcase</returns>
        public static string ToPascalCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToUpper(culture);
            }

            return char.ToUpper(str[0], culture) + str.Substring(1);
        }

        /// <summary>
        /// 如果字符串超过最大长度，则从该字符串的开头获取该字符串的子字符串。
        /// </summary>
        /// <exception cref="ArgumentNullException">如果str为空，则抛出</exception>
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }

        /// <summary>
        ///如果字符串超过最大长度，则从该字符串的开头获取该字符串的子字符串。
        ///如果字符串被截断，它会在字符串末尾添加“…”后缀。
        ///返回的字符串不能长于maxlength。
        /// </summary>
        /// <exception cref="ArgumentNullException">如果str为空，则抛出</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        ///如果字符串超过最大长度，则从该字符串的开头获取该字符串的子字符串。
        ///如果截断，它会将给定的<paramref name=“postfix”/>添加到字符串的末尾。
        ///返回的字符串不能长于maxlength。
        /// </summary>
        /// <exception cref="ArgumentNullException">如果str为空，则抛出</exception>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }
    }
}