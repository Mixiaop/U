using System.IO;

namespace U.Extension.Drawing
{
    public static partial class Extensions
    {
        public static byte[] ToBytes(string filePath)
        {
            byte[] photo_byte = null;
            using (FileStream fs =
            new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    photo_byte = br.ReadBytes((int)fs.Length);
                }
            }

            return photo_byte;
        }
    }
}
