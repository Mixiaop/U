using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Globalization;
using System.ComponentModel;
using U.ComponentModel;

namespace U
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 加密11位手机号
        /// </summary>
        /// <param name="mobileNumber">手机号</param>
        /// <returns></returns>
        public static string EncryptMobileNumber(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
                return mobileNumber;
            if (mobileNumber.Length != 11)
                return mobileNumber;
            string result = string.Empty;
            result = mobileNumber.Substring(0, 3);
            result += "****";
            result += mobileNumber.Substring(7);
            return result;
        }
        /// <summary>
        /// 确保字符串是电子邮箱格式，否则抛出异常
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns></returns>
        public static string EnsureEmailToThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);
            if (!IsValidEmail(output))
            {
                throw new Exception("Email 未验证");
            }
            return output;
        }

        /// <summary>
        /// 验证字符串是否是有效的电子邮箱
        /// </summary>
        /// <param name="email">需验证的字符串</param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// 验证字符串是否是有效的手机号码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsValidMobile(string mobile)
        {
            if (String.IsNullOrEmpty(mobile))
                return false;

            mobile = mobile.Trim();
            var result = Regex.IsMatch(mobile, @"^(1[3|4|5|8|7])\d{9}$", RegexOptions.IgnoreCase);
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

        /// <summary>
        /// 确保字符串不为null
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// 确保字符串不超过最大允许长度（缩短字符串）
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="maxLength">最大允许长度</param>
        /// <param name="postfix">添加在缩短字符串的后面，未缩短则不添加</param>
        /// <returns>如果超过允许长度，返回缩短后的，否则返回原字符串</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 确保字符串只包含数值
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <returns>返回只包含数值的字符串, 空字符串则返回null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取自定义TypeConverter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeConverter GetCustomTypeConverter(Type type)
        {

            //我们不能用以下代码来注册我们的自定义类型Descriptor
            //TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            //所以手工去做

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();

            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// 将一个值转换成另一个目标类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="destinationType">目标类型</param>
        /// <returns></returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将一个值转换成另一个目标类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// 将一个值转换成另一个目标类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="destinationType">目标类型</param>
        /// <param name="culture">culture</param>
        /// <returns></returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {

            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetCustomTypeConverter(sourceType);

                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);

                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);

                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);

                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }

            return value;
        }

        /// <summary>
        /// 获取当前应用的时间戳
        /// unix时间戳（从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数）
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime nowTime = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = nowTime.Subtract(startTime);
            string timeStamp = "";
            timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return long.Parse(timeStamp);
        }

        /// <summary>
        /// 获取唯一的随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRndString()
        {
            var rand = new Random();
            return DateTime.Now.ToString("yyyyMMddHHmmss") + rand.Next(999);
        }
    }
}
