using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Drawing;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Windows.Forms;
using System.Threading; 
using ImageResizer;
using Aspose.Words;
using U;
using U.Converters;
using U.Utilities.Web;

namespace U.Tests.Web.AsposeWords
{
    public partial class Tests : System.Web.UI.Page
    {
        private static readonly object s_lock = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Bitmap image = WebPreview.GetWebPreview(new Uri("http://localhost:2712/Evaluation/CareerAnchor/Chart.aspx?userid=1168"));

            ////保存图片数据  
            //image.Save(WebHelper.MapPath("/AsposeWords/hello.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);  

            IConverterService converterService = UPrimeEngine.Instance.Resolve<IConverterService>();
            //converterService.HtmlToImage("http://localhost:2712/Evaluation/CourseTest/Chart.aspx?userid=1168", 600, 350, WebHelper.MapPath("/AsposeWords/hello.jpg"));
            converterService.HtmlToImage("http://www.baidu.com", 800, 600, WebHelper.MapPath("/AsposeWords/hello.jpg"));
            Response.Write("sus");
            var fileName = WebHelper.MapPath("/AsposeWords/Hello.aspx");

            //var imgPath = WebHelper.MapPath("/AsposeWords/1.jpg");


            //var wordPath = WebHelper.MapPath("/AsposeWords/1.doc");

            ////保存word
            //Document doc = new Document();
            //DocumentBuilder builder = new DocumentBuilder(doc);
            //using (WebClient client = new WebClient())
            //{
            //    client.Encoding = System.Text.Encoding.UTF8;
            //    //var html = client.DownloadString("http://192.168.1.165:14480/views/tests/careerAnchor/results.html?v="+DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss"));
            //    var html = client.DownloadString("http://192.168.1.165:14480/views/tests/careerAnchor/results.html");
            
            //    builder.InsertHtml(html);
            //    doc.Save(wordPath);
            //}


            ////保存图片
            //Document word = new Document(wordPath);

            //word.Save(imgPath, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Jpeg));

            //int w = 600;
            //int h = 300;
            //int x = 10;
            //int y = 0;
            //lock (s_lock)
            //{
            //    var oriPictureBinary = System.IO.File.ReadAllBytes(imgPath);
            //    using (var stream = new System.IO.MemoryStream(oriPictureBinary))
            //    {
            //        Bitmap b = null;
            //        try
            //        {
            //            //try-catch 确保图片二进制正确
            //            b = new Bitmap(stream);
            //        }
            //        catch (ArgumentException exc)
            //        {
            //            Response.Write(string.Format("错误：剪切源图。{0}", exc.Message));
            //        }
            //        if (b == null)
            //        {
            //            //无法加载bitmap的一些原因
            //        }

            //        //目标流
            //        var destStream = new System.IO.MemoryStream();
            //        int bottom = w + Math.Abs(x);
            //        int right = h + Math.Abs(y);

            //        var resizeSettings = new ResizeSettings();
            //        resizeSettings.Mode = FitMode.Crop;
            //        resizeSettings.CropXUnits = 0;
            //        resizeSettings.CropYUnits = 0;
            //        resizeSettings.CropTopLeft = new PointF(x, y); //从哪开始
            //        resizeSettings.CropBottomRight = new PointF(x + h, y + w); //从哪开始
            //        //resizeSettings.Width = w;
            //        //resizeSettings.Height = h;
            //        //resizeSettings.Mode = FitMode.Crop;
            //        //resizeSettings.CropTopLeft = new PointF(x, y); //从哪开始
            //        ////resizeSettings.CropBottomRight = new PointF(w, h); //从哪结束
            //        ImageBuilder.Current.Build(b, destStream, resizeSettings);

            //        var destBinary = destStream.ToArray();

            //        System.IO.File.WriteAllBytes(WebHelper.MapPath("/AsposeWords/2.jpg"), destBinary);

            //        //if (!string.IsNullOrEmpty(oriPicture.MimeType))
            //        //    oriPicture.MimeType = oriPicture.MimeType.Trim();

            //        //picture = InsertPicture(destBinary, oriPicture.MimeType, oriPicture.SeoFilename, true);
            //        b.Dispose();
            //    }
            //}

        }
    }
}
