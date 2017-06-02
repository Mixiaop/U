using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    /// 获取Email的主机名称（@后面的地址）
    /// </summary>
    /// <param name="strEmail"></param>
    /// <returns></returns>
    public static string GetEmailHostName(this string strEmail)
    {
        if (strEmail.IndexOf("@") < 0)
        {
            return "";
        }
        return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
    }
}