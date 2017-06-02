using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace U.Tests.Web
{
    public partial class PhantomjsTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var driver = new ChromeDriver(U.Utilities.Web.WebHelper.MapPath("/"));
            driver.Url = "http://www.htmleaf.com/";
            
            var screen = driver.GetScreenshot();
            var bytes = screen.AsByteArray;
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            image.Save(U.Utilities.Web.WebHelper.MapPath("/hello.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            driver.Close();
            Response.Write("sus");

        }
    }
}