using System.Threading.Tasks;
using System.Net.Mail;
using U.Logging;

namespace U.Net.Mail
{
    public class NullEmailSender : EmailSenderBase
    {
        public ILogger Logger { get; set; }

        public NullEmailSender(IEmailSenderConfiguration config)
            : base(config)
        {
            Logger = NullLogger.Instance;
        }

        protected override Task SendEmailAsync(MailMessage mail)
        {
            Logger.Debug("SendEmailAsync:");
            LogEmail(mail);
            return Task.FromResult(0);
        }

        protected override void SendEmail(MailMessage mail)
        {
            Logger.Debug("SendEmail:");
            LogEmail(mail);
        }

        private void LogEmail(MailMessage mail)
        {
            Logger.Debug(mail.To.ToString());
            Logger.Debug(mail.CC.ToString());
            Logger.Debug(mail.Subject);
            Logger.Debug(mail.Body);
        }
    }
}
