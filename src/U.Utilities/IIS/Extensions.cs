using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Utilities.IIS
{
    public static class Extensions
    {
        /// <summary>
        /// 判断字符串是否相等
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static bool EqualsEx(this string text1, string text2)
        {
            return string.Equals(text1, text2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
