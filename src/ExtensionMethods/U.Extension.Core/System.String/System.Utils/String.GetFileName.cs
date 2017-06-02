
public static partial class Extensions
{
    /// <summary>
    /// 返回URL中结尾的文件名
    /// </summary>		
    public static string GetFilename(this string url)
    {
        if (url == null)
        {
            return "";
        }
        string[] strs1 = url.Split(new char[] { '/' });
        return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
    }
}