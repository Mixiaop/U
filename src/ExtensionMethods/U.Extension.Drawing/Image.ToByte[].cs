using System;
using System.Drawing;
using System.IO;

public static partial class Extensions
{
    public static byte[] ToBytes(this Image img)
    {

        byte[] bt = null;

        if (!img.Equals(null))
        {
            using (MemoryStream mostream = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(img);

                bmp.Save(mostream, System.Drawing.Imaging.ImageFormat.Jpeg);//将图像以指定的格式存入缓存内存流

                bt = new byte[mostream.Length];

                mostream.Position = 0;//设置留的初始位置

                mostream.Read(bt, 0, Convert.ToInt32(bt.Length));

            }

        }

        return bt;

    }
}