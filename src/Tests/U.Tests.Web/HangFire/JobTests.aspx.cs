using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.BackgroundJobs;

namespace U.Tests.Web.HangFire
{
    public partial class JobTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnOpen.Click += btnOpen_Click;
            btnDelete.Click += btnDelete_Click;
            
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            IBackgroundJobManager backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();
            backgroundJobManager.Delete(hidJobId.Value);
            Response.Write(hidJobId.Value + " 删除成功");
        }

        void btnOpen_Click(object sender, EventArgs e)
        {
            
            IBackgroundJobManager backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();
            TimeSpan timeSpan = DateTime.Now.AddMinutes(3) - DateTime.Now;
            var jobId = backgroundJobManager.Enqueue<TestJob, int>(1, BackgroundJobPriority.Normal, timeSpan);
            hidJobId.Value = jobId;
            Response.Write("JobId：" + jobId);
        }
    }

    public class TestJob : BackgroundJob<int> {
        public override void Execute(int args)
        {
            Console.Write("hello world");
        }
    }
}