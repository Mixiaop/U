using System;
using System.Text;

public static partial class Extensions
{
    /// <summary>
    /// 截断字符串
    /// </summary>
    /// <param name="strInput">字符串</param>
    /// <param name="intLen">长度</param>
    /// <param name="suffix">后缀（默认使用...）</param>
    /// <returns></returns>
    public static string CutString(this string strInput, int intLen, string suffix = "...")
    {
        if (string.IsNullOrEmpty(strInput))
        {
            return strInput;
        }
        strInput = strInput.Trim();
        if (Encoding.Default.GetBytes(strInput).Length <= intLen)
        {
            return strInput;
        }
        string str = "";
        for (int i = 0; i < strInput.Length; i++)
        {
            if (Encoding.Default.GetBytes(str + strInput.Substring(i, 1)).Length > intLen)
            {
                break;
            }
            str = str + strInput.Substring(i, 1);
        }
        return (str + suffix);
    }
}