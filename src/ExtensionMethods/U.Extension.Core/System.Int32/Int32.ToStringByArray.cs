
public  static partial class Extensions
{
    /// <summary>
    /// 返回int[]=>user.id, =>1,2,3,4
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToStringByArray(this int[] source)
    {
        string str = string.Empty;
        foreach (var i in source)
        {
            if (i != 0)
                str += i + ",";
        }
        if (str != string.Empty)
        {
            return str.TrimEnd(",");
        }
        else
        {
            return str;
        }
    }
}