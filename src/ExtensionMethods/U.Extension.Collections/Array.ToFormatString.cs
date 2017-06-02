using System.Text;

public static partial class Extensions
{
    public static string ToFormatString(this string[] @this, string packageStr = "", string segmentStr = ",")
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            sb.Append(packageStr + @this[i] + packageStr);
            if (i < (@this.Length - 1))
                sb.Append(segmentStr);

        }
        return sb.ToString();
    }

    public static string ToFormatString(this int[] @this, string segmentStr = ",")
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < @this.Length; i++)
        {
            sb.Append(@this[i].ToString());
            if (i < (@this.Length - 1))
                sb.Append(segmentStr);

        }
        return sb.ToString();
    }
}
