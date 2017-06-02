using System.Drawing;
using System.IO;

public static partial class Extensions
{
    public static Image ToImage(this byte[] bytes) {
        Image photo = null;
        using (MemoryStream ms = new MemoryStream(bytes)) {
            ms.Write(bytes, 0, bytes.Length);
            photo = Image.FromStream(ms, true);
        }
        return photo;
    }
}