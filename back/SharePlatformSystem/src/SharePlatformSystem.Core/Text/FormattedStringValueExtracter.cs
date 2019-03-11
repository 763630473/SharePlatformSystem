using SharePlatformSystem.Collections.Extensions;
using SharePlatformSystem.Core.Text.Formatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SharePlatformSystem.Core.Text
{
    /// <summary>
    ///此类用于从格式化字符串中提取动态值。
    ///与<see cref=“string.format（string，object）/>
    ///</summary>
    ///<example>
    ///说str是“我的名字是neo”，格式是“我的名字是名字.”。
    ///那么extract方法得到“neo”作为“name”。
    /// </example>
    public class FormattedStringValueExtracter
    {
        /// <summary>
        /// 从格式化字符串中提取动态值。
        /// </summary>
        /// <param name="str">包含动态值的字符串</param>
        /// <param name="format">字符串的格式</param>
        /// <param name="ignoreCase">是的，搜索不区分大小写。</param>
        /// <param name="splitformatCharacter">提供时使用此字符拆分格式。</param>
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
                            Debug.Assert(previousToken != null, "previoustoken不能为空，因为我在此处大于0");

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
        ///检查给定值是否符合给定值<see cref=“str”/>。
        ///还获取提取的值。
        /// </summary>
        /// <param name="str">包含动态值的字符串</param>
        /// <param name="format">字符串的格式</param>
        /// <param name="values">匹配的提取值数组</param>
        /// <param name="ignoreCase">是，搜索不区分大小写</param>
        /// <returns>如果匹配，则为真。</returns>
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
        /// 用作<see cref=“extract”/>方法的返回值。
        /// </summary>
        public class ExtractionResult
        {
            /// <summary>
            /// 完全匹配。
            /// </summary>
            public bool IsMatch { get; set; }

            /// <summary>
            /// 匹配的动态值列表。
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