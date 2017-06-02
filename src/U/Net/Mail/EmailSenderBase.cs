using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace U.Net.Mail
{
    /// <summary>
    /// 邮件发送者抽象类，实现IEmailSender
    /// </summary>
    public abstract class EmailSenderBase : IEmailSender
    {
        protected readonly IEmailSenderConfiguration _config;
        protected EmailSenderBase(IEmailSenderConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            var mail = new MailMessage(new MailAddress(_config.DefaultFromAddress, _config.DefaultFromDisplayName), new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            Send(mail);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        /// <returns></returns>
        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {

            var mail = new MailMessage(new MailAddress(from, _config.DefaultFromDisplayName), new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            Send(mail);
        }

        /// <summary>
        /// 发送邮箱
        /// </summary>
        /// <param name="mail">发送的信息</param>
        /// <param name="normalize">规范化（true=使用默认配置的邮箱当发送者信息）</param>
        public void Send(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            SendEmail(mail);
        }

        #region Async Methods
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        /// <returns></returns>
        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            var mail = new MailMessage(new MailAddress(_config.DefaultFromAddress, _config.DefaultFromDisplayName), new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            await SendAsync(mail);
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        /// <returns></returns>
        public async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            var mail = new MailMessage(new MailAddress(from, _config.DefaultFromDisplayName), new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            await SendAsync(mail);
        }



        /// <summary>
        /// 发送邮箱
        /// </summary>
        /// <param name="mail">发送的信息</param>
        /// <param name="normalize">规范化（true=使用默认配置的邮箱当发送者信息）</param>
        public async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            await SendEmailAsync(mail);
        }


        
        #endregion

        /// <summary>
        /// 发送邮箱抽象方法定义（如：SMTP实现等）
        /// </summary>
        /// <param name="mail"></param>
        protected abstract void SendEmail(MailMessage mail);

        /// <summary>
        /// 发送邮箱抽象方法定义（如：SMTP实现等）
        /// </summary>
        /// <param name="mail"></param>
        protected abstract Task SendEmailAsync(MailMessage mail);

        #region Utilities
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(
                    _config.DefaultFromAddress,
                    _config.DefaultFromDisplayName,
                    Encoding.UTF8
                    );
            }

            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
        #endregion
    }
}
