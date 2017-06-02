using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Aspose.Words;
using U.Utilities.Web;

namespace U.Tests.Web.Converters
{
    public partial class HtmlToWordTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //保存word
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                var html = client.DownloadString("http://localhost:1569/Converters/test.html");
                builder.InsertHtml(html);
                doc.Save(WebHelper.MapPath("/Converters/test.doc"));
            }

        }

        public void SaveWord()
        {
            

        }
    }
}