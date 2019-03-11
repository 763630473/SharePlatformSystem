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
                            throw new FormatException("�ַ������﷨����ȷ" + i + "! ��ʽ�ַ������ܰ���Ƕ�׵Ķ�ֵ̬���ʽ��");
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
                            throw new FormatException("�ַ������﷨����ȷ" + i + "! �ⲻ�������ŵ�������}.");
                        }

                        inDynamicValue = false;

                        if (currentText.Length <= 0)
                        {
                            throw new FormatException("�ַ������﷨����ȷ" + i + "! �����Ų������κ��ַ���");
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
                throw new FormatException("�򿪵��ַ�{û�н����ַ�}��");
            }

            if (currentText.Length > 0)
            {
                tokens.Add(new FormatStringToken(currentText.ToString(), FormatStringTokenType.ConstantText));
            }

            return tokens;
        }
    }
}