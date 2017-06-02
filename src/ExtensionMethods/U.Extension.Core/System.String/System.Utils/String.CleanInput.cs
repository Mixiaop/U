using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    /// 清理字符串
    /// </summary>
    public static string CleanInput(this string strIn)
    {
        return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
    }
}