using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Text.Formatting
{
    public class FormatStringTokenizer
    {
        public List<FormatStringToken> Tokenize(string format, bool includeBracketsForDynamicValues = false)
        {
            var tokens = new List<FormatStringToken>();

            var currentText = new StringBuilder();
            var inDynamicValue = false;

            for (var i = 0; i < format.Length; i++)
            {
                var c = format[i];
                switch (c)
                {
                    case '{':
                        if (inDynamicValue)
                        {
                            throw new FormatException("字符处的语法不正确" + i + "! 格式字符串不能包含嵌套的动态值表达式！");
                        }

                        inDynamicValue = true;

                        if (currentText.Length > 0)
                        {
                            tokens.Add(new FormatStringToken(currentText.ToString(), FormatStringTokenType.ConstantText));
                            currentText.Clear();
                        }

                        break;
                    case '}':
                        if (!inDynamicValue)
                        {
                            throw new FormatException("字符处的语法不正确" + i + "! 这不是右括号的左括号}.");
                        }

                        inDynamicValue = false;

                        if (currentText.Length <= 0)
                        {
                            throw new FormatException("字符处的语法不正确" + i + "! 方括号不包含任何字符。");
                        }

                        var dynamicValue = currentText.ToString();
                        if (includeBracketsForDynamicValues)
                        {
                            dynamicValue = "{" + dynamicValue + "}";
                        }

                        tokens.Add(new FormatStringToken(dynamicValue, FormatStringTokenType.DynamicValue));
                        currentText.Clear();

                        break;
                    default:
                        currentText.Append(c);
                        break;
                }
            }

            if (inDynamicValue)
            {
                throw new FormatException("打开的字符{没有结束字符}。");
            }

            if (currentText.Length > 0)
            {
                tokens.Add(new FormatStringToken(currentText.ToString(), FormatStringTokenType.ConstantText));
            }

            return tokens;
        }
    }
}