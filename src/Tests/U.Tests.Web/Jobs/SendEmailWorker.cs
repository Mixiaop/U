using U.Dependency;
using U.Threading.BackgroundWorkers;
using U.Threading.Timers;
using U.Net.Mail;

namespace U.Tests.Web.Jobs
{
    public class SendEmailWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IEmailSender _emailSender;
        public SendEmailWorker(UTimer timer, IEmailSender emailSender)
            : base(timer)
        {
            _emailSender = emailSender;
            Timer.Period = 30000; //5 seconds (good for tests, but normally will be more)
        }

        protected override void DoWork()
        {
            _emailSender.SendAsync("zhanghp@eagersoft.cn", "30周期发送任务", "30秒周期发送任务");
        }
    }
}