using System;
using System.Security.Cryptography;

namespace U.Utilities.Strings
{
    public static class StringHelper
    {
        /// <summary>
        /// 获取指定位数的随机字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int length) {
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijqlmnopqrtsuvwxyz0123456789";
            int max = s.Length - 1;
            if (length > max)
                length = max;
            var rand = new Random();
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += s.Substring(rand.Next(max), 1);
            }
            return result;
        }

        /// <summary>
        /// 生成随机的数字代码
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                str = String.Concat(str, random.Next(10).ToString());
            }
            return str;
        }

        /// <summary>
        /// 在指定的范围内生成随的int数字
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns></returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }
    }
}
