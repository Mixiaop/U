using System.Web;
using System.IO;

public static partial class Extensions
{
    /// <summary>
    /// 获取图片二进制数组
    /// </summary>
    /// <param name="postedFile"></param>
    /// <returns></returns>
    public static byte[] GetBits(this HttpPostedFile postedFile)
    {
        Stream fs = postedFile.InputStream;
        int size = postedFile.ContentLength;
        byte[] img = new byte[size];
        fs.Read(img, 0, size);
        return img;
    }
}
