using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace U.Converters.Utilities
{
    /// <summary>
    /// 通过自带的浏览器截屏类
    /// </summary>
    public class Screenshot
    {
        public static Image Take(string url, int width, int height)
        {
            var size = GetSize(width, height);
            return Take(url, size);
        }

        public static Image Take(string url, Size size)
        {
            var result = Capture(url, size);
            return result;
        }

        public static Image Save(string url, string path, ImageFormat format, int width, int height)
        {
            var size = GetSize(width, height);
            return Save(url, path, format, size);
        }

        public static Image Save(string url, string path, ImageFormat format, Size size)
        {
            var image = Take(url, size);

            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                var bytes = stream.ToArray();
                File.WriteAllBytes(path, bytes);

            }
            return image;
        }

        public static Size GetSize(int width, int height = 0)
        {
            return new Size(width, height);
        }

        private static Image Capture(string url, Size size)
        {
            Image result = new Bitmap(size.Width, size.Height);

            var thread = new Thread(() =>
            {
                using (var browser = new WebBrowser())
                {
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = true;
                    browser.Navigate(url);
                    browser.Width = size.Width;
                    browser.Height = size.Height;
                    browser.ScriptErrorsSuppressed = true;
                    browser.DocumentCompleted += (sender, args) => DocumentCompleted(sender, args, ref result);

                    while (browser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            return result;
        }

        private static void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e, ref Image image)
        {
            var browser = sender as WebBrowser;

            if (browser == null) throw new Exception("Sender should be browser");
            if (browser.Document == null) throw new Exception("Document is missing");
            if (browser.Document.Body == null) throw new Exception("Body is missing");

            using (var bitmap = new Bitmap(browser.Width, browser.Height))
            {
                browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
                image = (Image)bitmap.Clone();
            }
        }
    }
}
