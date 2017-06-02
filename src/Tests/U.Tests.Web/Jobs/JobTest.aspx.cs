using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.BackgroundJobs;

namespace U.Tests.Web.Jobs
{
    public partial class JobTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSendEmail.Click += btnSendEmail_Click;
        }

        void btnSendEmail_Click(object sender, EventArgs e)
        {
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            var args = new SendEmailJobArgs();
            args.ToEmail = "zhanghp@eagersoft.cn";
            args.Subject = "Hangfire 邮箱测试每天6点执行";
            args.Body = "Hangfire 邮箱测试每天6点执行";
            //backgroundJobManager.EnqueueAsync<SendEmailJob, SendMailErrorEventArgs>(args);
            //backgroundJobManager.AddRecurringJob<SendEmailJob, SendEmailJobArgs>(args, "");
            backgroundJobManager.ScheduleAsync<SendEmailJob, SendEmailJobArgs>(args, DateTime.Now.AddMinutes(3));

            Response.Write("成功了");
        }
    }
}