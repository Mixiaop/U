using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using U.Net.Mail.Configuration;
using U.Net.Mail;
using U.Net.Mail.Smtp;

namespace U.Tests.Web.Mail
{
    public partial class Sender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EmailSenderConfigInfo info = new EmailSenderConfigInfo();
            //EmailSenderConfigs.SaveConfig(info);
            btnSend.Click += btnSend_Click;
        }

        void btnSend_Click(object sender, EventArgs e)
        {
            //UPrime.Instance.Register<IEmailSender, HeybenEmailSender>(U.Dependency.DependencyLifeStyle.Transient);


            var emailSender = UPrimeEngine.Instance.Resolve<IEmailSender>();
            emailSender.SendAsync("zhanghp@eagersoft.cn", "【需求&BUG跟踪】【B端PC、H5】【测评、第三方登录】跟踪", "笔记更新 => <a href='http://note.youzy.cn/Notes/ContentInfo.aspx?contentid=682' target='_blank'>【B端PC、H5】【测评、第三方登录】跟踪】</a>");
            Response.Write("成功了");
        }
    }

    //public class HeybenEmailSender : SmtpEmailSender { 
    
    //}
}