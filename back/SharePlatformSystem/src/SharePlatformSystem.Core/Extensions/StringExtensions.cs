using Castle.Core.Internal;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SharePlatformSystem.Core.Extensions
{
    /// <summary>
    /// �ַ��������չ������
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///��������ַ���β�����ڸ����ַ����Ľ�β���һ���ַ���
        /// </summary>
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        /// ��������ַ���β�����ڸ����ַ����Ľ�β���һ���ַ���
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
        /// ��������ַ���β�����ڸ����ַ����Ľ�β���һ���ַ���
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
        /// ��������ַ����Ŀ�ͷ�������ַ���ͷ������������ַ���
        /// </summary>
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        ///��������ַ����Ŀ�ͷ�������ַ���ͷ������������ַ���
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
        /// ��������ַ����Ŀ�ͷ�������ַ���ͷ������������ַ���
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
        /// ָʾ���ַ����ǿ��ַ�������System.String.Empty�ַ�����
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// ָʾ���ַ����ǿ��ַ������ǽ��ɿո��ַ���ɡ�
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// ���ַ����Ŀ�ͷ��ȡ�ַ��������ַ�����
        /// </summary>
        /// <exception cref="ArgumentNullException">���<paramref name=��str��/>Ϊ��������</exception>
        /// <exception cref="ArgumentException">����ַ����ĳ��ȴ���</exception>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len�������ܴ��ڸ����ַ����ĳ��ȣ�");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// ���ַ����е���βת��Ϊ<see cref=��environment.newline��/>��
        /// </summary>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// ��ȡ�ַ����е�n���ַ����ֵ�������
        /// </summary>
        /// <param name="str">Ҫ������Դ�ַ���</param>
        /// <param name="c">Ҫ�������ַ�<see cref=��str��/></param>
        /// <param name="n">�����ļ���</param>
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
        ///�Ӹ����ַ�����ĩβɾ����һ�γ��ֵĸ�����׺��
        ///�������Ҫ���������һ���޸�ƥ�䣬�򲻻���������޸���
        /// </summary>
        /// <param name="str">�ַ�����</param>
        /// <param name="postFixes">һ��������׺.</param>
        /// <returns>�޸ĺ���ַ�������ͬ���ַ��������û���κθ����ĺ�׺��</returns>
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
        ///�Ӹ����ַ����Ŀ�ͷɾ����һ������ǰ׺��
        ///�������Ҫ���������һ��ǰ׺ƥ�䣬�򲻻��������ǰ׺��
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="preFixes">������ǰ׺��</param>
        /// <returns>�޸ĺ���ַ�������ͬ���ַ����������û���κθ���ǰ׺��</returns>
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
        ///���ַ�����β��ȡ�ַ��������ַ�����
        /// </summary>
        /// <exception cref="ArgumentNullException">strΪ�����쳣</exception>
        /// <exception cref="ArgumentException">���len�ĳ��ȴ����ַ����ĳ��ȣ����׳�</exception>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len�������ܴ��ڸ����ַ����ĳ��ȣ�");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        ///ʹ��string.split�����������ָ�����ָ������ַ�����
        /// </summary>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// ʹ��string.split�����������ָ�����ָ������ַ�����
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        ///ʹ��String.Split�����������ַ������Ϊ<see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        ///ʹ��String.Split�����������ַ������Ϊ <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// ��pascalcase�ַ���ת��Ϊcamelcase�ַ�����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="invariantCulture">������ culture</param>
        /// <returns>�շ��ַ���</returns>
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
        ///��ָ���������е�pascalcase�ַ���ת��Ϊcamelcase�ַ�����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="culture">�ṩ�ض��������ԵĴ�Сд����Ķ���</param>
        /// <returns>�շ��ַ���</returns>
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
        ///��������pascalcase/camelcase�ַ���ת��Ϊ���ӣ�ͨ�����ո��ֵ��ʣ���
        ///ʾ������thisIssampleSentence��ת��Ϊ��This is a sample sentence����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="invariantCulture">������culture</param>
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
        ///��������pascalcase/camelcase�ַ���ת��Ϊ���ӣ�ͨ�����ո��ֵ��ʣ���
        ///ʾ������thisIssampleSentence��ת��Ϊ��This is a sample sentence����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="culture">�ṩ�ض��������ԵĴ�Сд����Ķ���</param>
        public static string ToSentenceCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1], culture));
        }

        /// <summary>
        ///���ַ���ת��Ϊö��ֵ��
        /// </summary>
        /// <typeparam name="T">ö�ٵ�����</typeparam>
        /// <param name="value">Ҫת�����ַ���</param>
        /// <returns>����ö�ٶ���</returns>
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
        /// ���ַ���ת��Ϊö��ֵ��
        /// </summary>
        /// <typeparam name="T">ö�ٵ�����</typeparam>
        /// <param name="value">Ҫת�����ַ���</param>
        /// <param name="ignoreCase">���԰���</param>
        /// <returns>����ö�ٶ���</returns>
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
        /// ��camelcase�ַ���ת��Ϊpascalcase�ַ�����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="invariantCulture">���� culture</param>
        /// <returns>�ַ�����Pascalcase</returns>
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
        /// ��ָ���������е�camelcase�ַ���ת��Ϊpascalcase�ַ�����
        /// </summary>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <param name="culture">�ṩ�ض��������ԵĴ�Сд����Ķ���</param>
        /// <returns>�ַ�����Pascalcase</returns>
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
        /// ����ַ���������󳤶ȣ���Ӹ��ַ����Ŀ�ͷ��ȡ���ַ��������ַ�����
        /// </summary>
        /// <exception cref="ArgumentNullException">���strΪ�գ����׳�</exception>
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
        ///����ַ���������󳤶ȣ���Ӹ��ַ����Ŀ�ͷ��ȡ���ַ��������ַ�����
        ///����ַ������ضϣ��������ַ���ĩβ��ӡ�������׺��
        ///���ص��ַ������ܳ���maxlength��
        /// </summary>
        /// <exception cref="ArgumentNullException">���strΪ�գ����׳�</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        ///����ַ���������󳤶ȣ���Ӹ��ַ����Ŀ�ͷ��ȡ���ַ��������ַ�����
        ///����ضϣ����Ὣ������<paramref name=��postfix��/>��ӵ��ַ�����ĩβ��
        ///���ص��ַ������ܳ���maxlength��
        /// </summary>
        /// <exception cref="ArgumentNullException">���strΪ�գ����׳�</exception>
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