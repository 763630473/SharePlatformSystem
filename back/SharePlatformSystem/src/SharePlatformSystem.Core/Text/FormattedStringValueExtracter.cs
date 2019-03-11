using SharePlatformSystem.Collections.Extensions;
using SharePlatformSystem.Core.Text.Formatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SharePlatformSystem.Core.Text
{
    /// <summary>
    ///�������ڴӸ�ʽ���ַ�������ȡ��ֵ̬��
    ///��<see cref=��string.format��string��object��/>
    ///</summary>
    ///<example>
    ///˵str�ǡ��ҵ�������neo������ʽ�ǡ��ҵ�����������.����
    ///��ôextract�����õ���neo����Ϊ��name����
    /// </example>
    public class FormattedStringValueExtracter
    {
        /// <summary>
        /// �Ӹ�ʽ���ַ�������ȡ��ֵ̬��
        /// </summary>
        /// <param name="str">������ֵ̬���ַ���</param>
        /// <param name="format">�ַ����ĸ�ʽ</param>
        /// <param name="ignoreCase">�ǵģ����������ִ�Сд��</param>
        /// <param name="splitformatCharacter">�ṩʱʹ�ô��ַ���ָ�ʽ��</param>
        public ExtractionResult Extract(string str, string format, bool ignoreCase = false, char? splitformatCharacter = null)
        {
            var stringComparison = ignoreCase
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

            if (str == format)
            {
                return new ExtractionResult(true);
            }

            var formatTokens = TokenizeFormat(format, splitformatCharacter);

            if (formatTokens.IsNullOrEmpty())
            {
                return new ExtractionResult(str == "");
            }

            var result = new ExtractionResult(false);

            for (var i = 0; i < formatTokens.Count; i++)
            {
                var currentToken = formatTokens[i];
                var previousToken = i > 0 ? formatTokens[i - 1] : null;

                if (currentToken.Type == FormatStringTokenType.ConstantText)
                {
                    if (i == 0)
                    {
                        if (str.StartsWith(currentToken.Text, stringComparison))
                        {
                            str = str.Substring(currentToken.Text.Length);
                        }
                    }
                    else
                    {
                        var matchIndex = str.IndexOf(currentToken.Text, stringComparison);
                        if (matchIndex >= 0)
                        {
                            Debug.Assert(previousToken != null, "previoustoken����Ϊ�գ���Ϊ���ڴ˴�����0");

                            result.Matches.Add(new NameValue(previousToken.Text, str.Substring(0, matchIndex)));
                            result.IsMatch = true;
                            str = str.Substring(matchIndex + currentToken.Text.Length);
                        }
                    }
                }
            }

            var lastToken = formatTokens.Last();
            if (lastToken.Type == FormatStringTokenType.DynamicValue)
            {
                result.Matches.Add(new NameValue(lastToken.Text, str));
                result.IsMatch = true;
            }

            return result;
        }

        private List<FormatStringToken> TokenizeFormat(string originalFormat, char? splitformatCharacter = null)
        {
            if (splitformatCharacter == null)
            {
                return new FormatStringTokenizer().Tokenize(originalFormat);
            }

            var result = new List<FormatStringToken>();
            var formats = originalFormat.Split(splitformatCharacter.Value);

            foreach (var format in formats)
            {
                result.AddRange(new FormatStringTokenizer().Tokenize(format));
            }

            return result;
        }

        /// <summary>
        ///������ֵ�Ƿ���ϸ���ֵ<see cref=��str��/>��
        ///����ȡ��ȡ��ֵ��
        /// </summary>
        /// <param name="str">������ֵ̬���ַ���</param>
        /// <param name="format">�ַ����ĸ�ʽ</param>
        /// <param name="values">ƥ�����ȡֵ����</param>
        /// <param name="ignoreCase">�ǣ����������ִ�Сд</param>
        /// <returns>���ƥ�䣬��Ϊ�档</returns>
        public static bool IsMatch(string str, string format, out string[] values, bool ignoreCase = false)
        {
            var result = new FormattedStringValueExtracter().Extract(str, format, ignoreCase);
            if (!result.IsMatch)
            {
                values = new string[0];
                return false;
            }

            values = result.Matches.Select(m => m.Value).ToArray();
            return true;
        }

        /// <summary>
        /// ����<see cref=��extract��/>�����ķ���ֵ��
        /// </summary>
        public class ExtractionResult
        {
            /// <summary>
            /// ��ȫƥ�䡣
            /// </summary>
            public bool IsMatch { get; set; }

            /// <summary>
            /// ƥ��Ķ�ֵ̬�б�
            /// </summary>
            public List<NameValue> Matches { get; private set; }

            internal ExtractionResult(bool isMatch)
            {
                IsMatch = isMatch;
                Matches = new List<NameValue>();
            }
        }
    }
}