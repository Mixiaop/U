using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageResizer;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA;
using U.Utilities.Web;

namespace U.Converters
{
    public class ConverterService : IConverterService
    {
        /// <summary>
        /// Html 转 image 对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="savePath">是否保存到路径</param>
        /// <param name="format">保存图片时使用</param>
        /// <returns></returns>
        public Image HtmlToImage(string url, int width, int height, string savePath = "", ImageFormat format = null)
        {
            Image img = null;
            //if (savePath.IsNotNullOrEmpty())
            //{
            //    img = Screenshot.Save(url, savePath, (format != null ? format : ImageFormat.Jpeg), width, height);
            //}
            //else
            //{
            //    img = Screenshot.Take(url, width, height);
            //}

            using (var driver = new ChromeDriver(WebHelper.MapPath("/")))
            {
                driver.Url = url;
                System.Threading.Thread.Sleep(1000);
                var screen = driver.GetScreenshot();
                var bytes = screen.AsByteArray;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    if (width > 0 && height > 0)
                    {
                        var resizeSettings = new ResizeSettings();
                        resizeSettings.Mode = FitMode.Crop;
                        resizeSettings.CropXUnits = 0;
                        resizeSettings.CropYUnits = 0;
                        resizeSettings.CropTopLeft = new PointF(0, 0); //从哪开始
                        resizeSettings.CropBottomRight = new PointF(width, height); //从哪开始

                        Bitmap b = new Bitmap(ms);
                        var destStream = new System.IO.MemoryStream();
                        ImageBuilder.Current.Build(b, destStream, resizeSettings);
                        img = Image.FromStream(destStream);
                        img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        img = Image.FromStream(ms);
                        img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                }
                driver.Close();
            }

            return img;
        }

        /// <summary>
        /// Html url 转 image 对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="savePath">是否保存到路径</param>
        /// <param name="format">保存图片时使用</param>
        /// <returns></returns>
        public byte[] HtmlToImageArray(string url, int width, int height, string savePath = "", ImageFormat format = null)
        {
            Image img = HtmlToImage(url, width, height, savePath, format);

            return img.ToBytes();
        }

    }
}
