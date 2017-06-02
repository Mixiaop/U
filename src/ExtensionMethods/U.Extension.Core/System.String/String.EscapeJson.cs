
public static partial class Extensions
{
    /// <summary>
    /// Json字符串转义
    /// </summary>
    /// <param name="s">字符串</param>
    /// <returns></returns>
    public static string EscapeJson(this string s)
    {
        if ((s != null) && (s.Length != 0))
        {
            s = s.Replace(@"\", @"\\");
            s = s.Replace("\"", "\\\"");
            s = s.Replace("\r", @"\r");
            s = s.Replace("\n", @"\n");
        }
        return s;
    }
}
