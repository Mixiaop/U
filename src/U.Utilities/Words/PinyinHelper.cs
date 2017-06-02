using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using U.Utilities.Words.Format;

namespace U.Utilities.Words
{
    /// <summary>
    /// 中文汉字转拼音帮助类
    /// </summary>
    public class PinyinHelper
    {
        private static Dictionary<string, string> dict;

        private PinyinHelper()
        {
        }

        static PinyinHelper()
        {
            dict = new Dictionary<string, string>();
            var doc = XDocument.Load(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "U.Utilities.Words.Resources.unicode_to_hanyu_pinyin.xml"));
            var query =
                from item in doc.Root.Descendants("item")
                select new
                {
                    Unicode = (string)item.Attribute("unicode"),
                    Hanyu = (string)item.Attribute("hanyu")
                };
            foreach (var item in query)
                if (item.Hanyu.Length > 0)
                    dict.Add(item.Unicode, item.Hanyu);

        }

        public static string ToPinyin(string input, string splitChar = " ")
        {
            string output = "";
            foreach (var s in input)
            {
                output += ToPinyin(s) + splitChar;
            }

            return output.Trim();
        }

        public static string ToPinyin(char ch)
        {
            string output = "";
            var list = ToPinyinArray(ch);
            if (list != null)
            {
                foreach (var s in list)
                {
                    output = s;
                    break;
                }
            }

            return output;
        }

        public static string[] ToPinyinArray(char ch)
        {
            return GetFomattedHanyuPinyinStringArray(ch, new PinyinOutputFormat());
        }

        /// <summary>
        /// 返回拼音字符数组（因有此中文会有两三种拼音或单调）
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string[] ToPinyinArray(char ch, PinyinOutputFormat format)
        {
            return GetFomattedHanyuPinyinStringArray(ch, format);
        }

        #region Private Functions
        private static string[] GetFomattedHanyuPinyinStringArray(
            char ch, PinyinOutputFormat format)
        {
            string[] unformattedArr = GetUnformattedHanyuPinyinStringArray(ch);
            if (null != unformattedArr)
            {
                for (int i = 0; i < unformattedArr.Length; i++)
                {
                    unformattedArr[i] = PinyinFormatter.FormatHanyuPinyin(unformattedArr[i], format);
                }
            }

            return unformattedArr;
        }

        private static string[] GetUnformattedHanyuPinyinStringArray(char ch)
        {
            string code = String.Format("{0:X}", (int)ch).ToUpper();
#if DEBUG
            Console.WriteLine(code);
#endif
            if (dict.ContainsKey(code))
            {
                return dict[code].Split(',');
            }

            return null;
        }
        #endregion
    }
}
