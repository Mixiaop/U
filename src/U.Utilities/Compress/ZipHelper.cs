using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace U.Utilities.Compress
{
    /// <summary>
    /// 压缩类
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="needZipDirectoryPath">需要压缩的文件夹（绝对路径）</param>
        /// <param name="zipedPath">压缩后的文件路径（绝对路径）</param>
        /// <param name="zipedFileName">压缩后的文件名称（文件名，默认 同源文件夹同名）</param>
        /// <param name="encryptPassword">加密密码（默认不加密）</param>
        public static void ZipDirectory(string needZipDirectoryPath, string zipedPath, string zipedFileName = "", string encryptPassword = "")
        {
            if (!System.IO.Directory.Exists(needZipDirectoryPath))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + needZipDirectoryPath + " 不存在!");
            }

            //文件名称（默认同源文件名称相同）
            string ZipFileName = string.IsNullOrEmpty(zipedFileName) ? zipedPath + "\\" + new DirectoryInfo(needZipDirectoryPath).Name + ".zip" : zipedPath + "\\" + zipedFileName + ".zip";

            using (System.IO.FileStream ZipFile = System.IO.File.Create(ZipFileName))
            {
                using (ZipOutputStream s = new ZipOutputStream(ZipFile))
                {
                    if (encryptPassword.IsNotNullOrEmpty())
                    {
                        //压缩文件加密
                        s.Password = encryptPassword;
                    }
                    Zip(needZipDirectoryPath, s, "");
                }
            }
        }

        #region Private
        /// <summary>
        /// 递归遍历目录
        /// </summary>
        private static void Zip(string strDirectory, ZipOutputStream s, string parentPath)
        {
            if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
            {
                strDirectory += Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();

            string[] filenames = Directory.GetFileSystemEntries(strDirectory);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {

                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string pPath = parentPath;
                    pPath += file.Substring(file.LastIndexOf("\\") + 1);
                    pPath += "\\";
                    Zip(file, s, pPath);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    using (FileStream fs = File.OpenRead(file))
                    {

                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);

                        string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(fileName);

                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;
                        
                        fs.Close();

                        crc.Reset();
                        crc.Update(buffer);

                        entry.Crc = crc.Value;
                        s.PutNextEntry(entry);

                        s.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        #endregion
    }

    /// <summary>
    ///  解压类
    /// </summary>
    public class UnZipHelper
    {
        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="zipFile">待解压的文件</param>
        /// <param name="unzipPath">指定解压目标目录</param>
        public static void UnZip(string zipFile, string unzipPath)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(@zipFile.Trim()));
            ZipEntry theEntry;

            if (!Directory.Exists(unzipPath))
            {
                Directory.CreateDirectory(unzipPath);
            }

            while ((theEntry = s.GetNextEntry()) != null)
            {
                ;
                string fileName = Path.GetFileName(theEntry.Name);

                if (fileName != String.Empty)
                {
                    FileStream streamWriter = File.Create(unzipPath + "\\" + theEntry.Name);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }

                    streamWriter.Close();
                }
                else
                {
                    Directory.CreateDirectory(unzipPath + "\\" + theEntry.Name);
                }
            }
            s.Close();
        }
    }
}
