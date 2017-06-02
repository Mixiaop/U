using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Net;

namespace U.Tests.Web.Utilities.Net
{
    public partial class WebRequestHelperTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpGet1.Click += HttpGet1_Click;
            HttpGet2.Click += HttpGet2_Click;
            HttpPost.Click += HttpPost_Click;
        }

        void HttpPost_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("key", "d27b1eb4e55744729be0e4ec5657531c");
            formData.Add("key2", "123123");
            formData.Add("key3", "123123");
            formData.Add("key4", "123123");
            formData.Add("key5", "123123");
            formData.Add("key6", "123123");
            formData.Add("key7", "123123");
            formData.Add("key8", "123123");
            formData.Add("key9", "123123");
            formData.Add("key10", "123123");
            formData.Add("key11", "123123");
            formData.Add("key12", "123123");
            var res = WebRequestHelper.HttpPost("http://localhost:1569/Utilities/Net/WebRequestHelperPostReceiveTests.aspx", formData);
            Response.Write("res：" + res);
        }

        void HttpGet2_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Auth", "123123");
            headers.Add("Sign", "321321");
            var html = WebRequestHelper.HttpGet("http://www.mbjuan.com", headers);
            Response.Write(html);
        }

        void HttpGet1_Click(object sender, EventArgs e)
        {
            var html =  WebRequestHelper.HttpGet("http://www.mbjuan.com",null);
            Response.Write(html);
        }
    }
}