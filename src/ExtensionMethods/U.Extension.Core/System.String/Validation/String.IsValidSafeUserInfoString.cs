using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    /// 检测是否有危险的可能用于链接的字符串
    /// </summary>
    /// <param name="str">要判断字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsValidSafeUserInfoString(this string str)
    {
        return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
    }
}