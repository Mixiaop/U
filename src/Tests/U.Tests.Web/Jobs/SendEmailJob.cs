using U.Dependency;
using U.Net.Mail;
using U.BackgroundJobs;

namespace U.Tests.Web.Jobs
{
    public class SendEmailJob : BackgroundJob<SendEmailJobArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        public SendEmailJob(IEmailSender emailSender) {
            _emailSender = emailSender;
        }
        public override void Execute(SendEmailJobArgs args)
        {
            _emailSender.SendAsync(args.ToEmail, args.Subject, args.Body);
        }
    }
}