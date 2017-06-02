using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace U.Utilities.Words.Format
{
    internal class PinyinFormatter
    {
        internal static string FormatHanyuPinyin(
            string pinyin, PinyinOutputFormat outputFormat)
        {
            if (outputFormat == null)
                throw new ArgumentNullException("The parameter outputFormat could not be null.");

            if (outputFormat.ToneType == PinyinToneType.WITH_TONE_MARK
                && (outputFormat.VCharType == PinyinVCharType.WITH_U_AND_COLON ||
                outputFormat.VCharType == PinyinVCharType.WITH_V))
                throw new ArgumentNullException("Tone marks cannot be added to v or u:");

            string result = pinyin.ToLower();

            if (outputFormat.ToneType == PinyinToneType.WITHOUT_TONE)
                result = Regex.Replace(pinyin, "[0-9]", "");
            else if (outputFormat.ToneType == PinyinToneType.WITH_TONE_MARK)
                result = ConvertToneNumber2ToneMark(result.Replace("u:", "v"));

            if (outputFormat.VCharType == PinyinVCharType.WITH_V)
                result = result.Replace("u:", "v");
            else if (outputFormat.VCharType == PinyinVCharType.WITH_U_UNICODE)
                result = result.Replace("u:", "ü");

            if (outputFormat.CaseType == PinyinCaseType.UPPERCASE)
                result = result.ToUpper();

            return result;
        }

        #region Private Functions
        private static string ConvertToneNumber2ToneMark(
            string pinyinWithToneNumber)
        {
            string result = pinyinWithToneNumber.ToLower();
            result = result.Replace("u:", "ü");
            result = result.Replace("v", "ü");
            KeyValuePair<string, List<string>> aRule = CreateReplaceRule("a", "āáǎàa");
            KeyValuePair<string, List<string>> oRule = CreateReplaceRule("o", "ōóǒòo");
            KeyValuePair<string, List<string>> eRule = CreateReplaceRule("e", "ēéěèe");
            KeyValuePair<string, List<string>> iRule = CreateReplaceRule("i", "īíǐìi");
            KeyValuePair<string, List<string>> uRule = CreateReplaceRule("u", "ūúǔùu");
            KeyValuePair<string, List<string>> vRule = CreateReplaceRule("ü", "ǖǘǚǜü");
            KeyValuePair<string, List<string>> iuRule = CreateReplaceRule("iu", "iūiúiǔiùiu");
            KeyValuePair<string, List<string>> uiRule = CreateReplaceRule("ui", "uīuíuǐuìui");
            KeyValuePair<string, List<string>>[] rules =
                new KeyValuePair<string, List<string>>[] {
                    aRule, oRule, eRule, iuRule, uiRule, iRule, uRule, vRule
                };

            
            if (Regex.IsMatch(result, "^[a-zü]*[1-5]$"))
            {
                int toneNumber = Int32.Parse(result.Last().ToString());
                result = Regex.Replace(result, "[1-5]", "");
#if DEBUG
                Console.WriteLine("The tone number of " + pinyinWithToneNumber + "is: " + toneNumber);
#endif
                try
                {
                    for (int i = 0; i < rules.Length; i++)
                    {
                        if (result.IndexOf(rules[i].Key) > -1)
                            return ConvertPinyinToToneMarkImpl(result, toneNumber, rules[i]);
                    }
                }
                catch (Exception)
                {
                    //  Nothing we can do
                }
            }
            return result;
        }

        private static KeyValuePair<string, List<string>> CreateReplaceRule(
            string ruleName, string ruleString)
        {
            KeyValuePair<string, List<string>> result = 
                new KeyValuePair<string, List<string>>(ruleName, new List<string>());

            int i = 0;
            while (i < ruleString.Length)
            {
                result.Value.Add(ruleString.Substring(i, ruleName.Length));
                i += ruleName.Length;
            }

            return result;
        }

        private static string ConvertPinyinToToneMarkImpl(
            string pinyin, int toneNumber, KeyValuePair<string, List<string>> rule)
        {
            if (rule.Value == null || toneNumber < 1
                || toneNumber > rule.Value.Count)
                throw new ArgumentOutOfRangeException("Invalid tone number!");

            string replacement = rule.Value[toneNumber - 1];
            return pinyin.Replace(rule.Key, replacement);
        }
        #endregion
    }
}
