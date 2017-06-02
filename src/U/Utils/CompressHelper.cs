using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace U.Utils
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    public class CompressHelper
    {
        /// <summary>
        /// GZip方式压缩
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream();
            using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true))
            {
                stream2.Write(bytes, 0, (int)bytes.Length);
            }
            stream.Position = (long)0L;

            byte[] buffer2 = new byte[stream.Length];
            stream.Read(buffer2, 0, (int)buffer2.Length);
            byte[] buffer3 = new byte[buffer2.Length + 4];
            Buffer.BlockCopy(buffer2, 0, buffer3, 4, (int)buffer2.Length);
            Buffer.BlockCopy(BitConverter.GetBytes((int)bytes.Length), 0, buffer3, 0, 4);
            return Convert.ToBase64String(buffer3);
        }

        /// <summary>
        /// GZip方式解压
        /// </summary>
        /// <param name="compressedText"></param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] buffer = Convert.FromBase64String(compressedText);
            using (MemoryStream stream = new MemoryStream())
            {
                int num = BitConverter.ToInt32(buffer, 0);
                stream.Write(buffer, 4, (int)(buffer.Length - 4));
                byte[] buffer2 = new byte[num];
                stream.Position = (long)0L;
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    stream2.Read(buffer2, 0, (int)buffer2.Length);
                }
                return Encoding.UTF8.GetString(buffer2);
            }
        }
    }
}
