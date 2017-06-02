using System.Net.Mail;

namespace U.Net.Mail.Smtp
{
    /// <summary>
    /// 使用 SMTP 发送邮件
    /// </summary>
    public interface ISmtpEmailSender
    {
        /// <summary>
        /// 建立 SMTP 客户端对象发送邮件
        /// </summary>
        /// <returns></returns>
        SmtpClient BuildClient();
    }
}
