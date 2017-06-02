using System;
using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    /// string型转为decimal
    /// </summary>
    /// <param name="expression">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static decimal ToDecimal(this string expression, decimal defValue = 0)
    {
        if (string.IsNullOrEmpty(expression))
            return defValue;
        try
        {
            return Math.Round(Convert.ToDecimal(expression), 2);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// string型转换为bool型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的bool类型结果</returns>
    public static bool ToBool(this string expression, bool defValue = false)
    {
        if (expression != null)
        {
            if (string.Compare(expression, "true", true) == 0)
                return true;
            else if (string.Compare(expression, "false", true) == 0)
                return false;
        }
        return defValue;
    }

    /// <summary>
    /// 将对象转换为Int32类型
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static int ToInt(this string str, int defValue = 0)
    {
        if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            return defValue;

        int rv;
        if (Int32.TryParse(str, out rv))
            return rv;

        return Convert.ToInt32(ToFloat(str, defValue));
    }

    /// <summary>
    /// string型转换为float型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static float ToFloat(this string strValue, float defValue = 0)
    {
        if ((strValue == null) || (strValue.Length > 10))
            return defValue;

        float intValue = defValue;
        if (strValue != null)
        {
            bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (IsFloat)
                float.TryParse(strValue, out intValue);
        }
        return intValue;
    }

    /// <summary>
    /// 将对象转换为日期时间类型
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static DateTime ToDateTime(this string str, DateTime defValue)
    {
        if (!string.IsNullOrEmpty(str))
        {
            DateTime dateTime;
            if (DateTime.TryParse(str, out dateTime))
                return dateTime;
        }
        return defValue;
    }

    /// <summary>
    /// 将对象转换为日期时间类型
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <returns>转换后的int类型结果</returns>
    public static DateTime StrToDateTime(this string str)
    {
        return ToDateTime(str, DateTime.Now);
    }
}