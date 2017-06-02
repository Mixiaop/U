using U.Settings;

namespace U.Net.Mail
{
    /// <summary>
    /// 邮箱发送配置信息
    /// </summary>
    public class EmailSenderSettings : USettings<EmailSenderSettings>
    {
        /// <summary>
        /// 默认邮箱地址（来自）
        /// </summary>
        public string DefaultFromAddress { get; set; }

        /// <summary>
        /// 默认邮箱显示名称（来自）
        /// </summary>
        public string DefaultFromDisplayName { get; set; }

        /// <summary>
        /// SMTP Host name/IP
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// SMTP Port
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// SMTP 用户名
        /// </summary>
        public string SmtpUsername { get; set; }

        /// <summary>
        /// SMTP 密码
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// SMTP 域名,可为空
        /// </summary>
        public string SmtpDomain { get; set; }

        /// <summary>
        /// 是否使用SSL（默认使用）
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 是否使用默认凭据（默认不使用）
        /// </summary>
        public bool UseDefaultCredentials { get; set; }
    }
}
