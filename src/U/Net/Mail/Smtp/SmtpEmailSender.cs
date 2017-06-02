using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using U.Dependency;

namespace U.Net.Mail.Smtp
{
    /// <summary>
    /// SMTP 邮件发送实现
    /// </summary>
    public class SmtpEmailSender : EmailSenderBase, ISmtpEmailSender, ITransientDependency
    {
        public SmtpEmailSender(IEmailSenderConfiguration config)
            : base(config)
        {
        }

        public SmtpClient BuildClient()
        {
            var host = _config.SmtpHost;
            var port = _config.SmtpPort;

            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (_config.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (_config.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;

                    var username = _config.SmtpUsername;

                    if (!username.IsNullOrEmpty())
                    {
                        var password = _config.SmtpPassword;
                        var domain = _config.SmtpDomain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(username, password, domain)
                            : new NetworkCredential(username, password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        protected override void SendEmail(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                smtpClient.Send(mail);
            }
        }

        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}
