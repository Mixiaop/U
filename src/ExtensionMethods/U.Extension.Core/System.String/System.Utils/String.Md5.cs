using System.Text;
using System.Security.Cryptography;
public static partial class Extensions
{
    public static string Md5(this string str)
    {
        byte[] b = Encoding.UTF8.GetBytes(str);
        b = new MD5CryptoServiceProvider().ComputeHash(b);
        string ret = "";
        for (int i = 0; i < b.Length; i++)
        {
            ret += b[i].ToString("x").PadLeft(2, '0');
        }

        return ret;
    }
}
