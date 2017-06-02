using System.Text;

public static partial class Extensions
{
    /// <summary>
    /// 生成指定数量的html空格符号
    /// </summary>
    public static string GetSpacesString(this int spacesCount)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < spacesCount; i++)
        {
            sb.Append(" &nbsp;&nbsp;");
        }
        return sb.ToString();
    }
}