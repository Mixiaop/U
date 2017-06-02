using System;

public static partial class Extensions
{
    /// <summary>
    /// object型转为decimal
    /// </summary>
    /// <param name="expression">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static decimal ToDecimal(this object expression, decimal defValue = 0)
    {
        if (expression == null)
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
    /// 将对象转换为bool类型
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static bool ToBool(this object expression)
    {
        bool value;
        if (expression != null)
        {
            try
            {
                value = (bool)expression;
            }
            catch
            {
                if ((int)expression == 0)
                    value = false;
                else value = true;
            }
        }
        else
        {
            return false;
        }
        return value;
    }

    /// <summary>
    /// 将对象转换为Int32类型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static int ToInt(this object expression, int defValue = 0)
    {
        return expression.IfNotNull(x => x.ToString().ToInt(), defValue);
    }

    /// <summary>
    /// string型转换为float型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static float ToFloat(this object expression, float defValue = 0)
    {
        return expression.IfNotNull(x => x.ToString().ToFloat(), defValue);
    }

    /// <summary>
    /// 将对象转换为日期时间类型
    /// </summary>
    /// <param name="obj">要转换的对象</param>
    /// <returns>转换后的int类型结果</returns>
    public static DateTime ToDateTime(this object obj)
    {
        return obj.ToString().StrToDateTime();
    }
}
