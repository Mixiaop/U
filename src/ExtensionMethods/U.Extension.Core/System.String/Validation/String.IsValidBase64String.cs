using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    /// 判断是否为base64字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsValidBase64String(this string str)
    {
        //A-Z, a-z, 0-9, +, /, =
        return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
    }
}