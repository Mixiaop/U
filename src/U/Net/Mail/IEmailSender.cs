using System.Threading.Tasks;
using System.Net.Mail;

namespace U.Net.Mail
{
    /// <summary>
    /// 定义简单发送邮箱的服务
    /// </summary>
    public interface IEmailSender
    {
       
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        void Send(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        /// <returns></returns>
        void Send(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送邮箱
        /// </summary>
        /// <param name="mail">发送的信息</param>
        /// <param name="normalize">规范化（true=使用默认配置的邮箱当发送者信息）</param>
        void Send(MailMessage mail, bool normalize = true);

        #region Async
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        /// <returns></returns>
        Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">目标邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否包含HTML</param>
        /// <returns></returns>
        Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送邮箱
        /// </summary>
        /// <param name="mail">发送的信息</param>
        /// <param name="normalize">规范化（true=使用默认配置的邮箱当发送者信息）</param>
        Task SendAsync(MailMessage mail, bool normalize = true);
        #endregion
    }
}
