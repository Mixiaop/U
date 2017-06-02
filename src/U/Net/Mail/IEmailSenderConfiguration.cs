
namespace U.Net.Mail
{
    /// <summary>
    /// Defines configurations used while send emails.
    /// </summary>
    public interface IEmailSenderConfiguration
    {
        /// <summary>
        /// 默认邮箱地址（来自）
        /// </summary>
        string DefaultFromAddress { get;  }

        /// <summary>
        /// 默认邮箱显示名称（来自）
        /// </summary>
        string DefaultFromDisplayName { get;  }

        /// <summary>
        /// SMTP Host name/IP
        /// </summary>
        string SmtpHost { get;  }

        /// <summary>
        /// SMTP Port
        /// </summary>
        int SmtpPort { get;  }

        /// <summary>
        /// SMTP 用户名
        /// </summary>
        string SmtpUsername { get;  }

        /// <summary>
        /// SMTP 密码
        /// </summary>
        string SmtpPassword { get;  }

        /// <summary>
        /// SMTP 域名,可为空
        /// </summary>
        string SmtpDomain { get;  }

        /// <summary>
        /// 是否使用SSL（默认使用）
        /// </summary>
        bool EnableSsl { get;  }

        /// <summary>
        /// 是否使用默认凭据（默认不使用）
        /// </summary>
        bool UseDefaultCredentials { get;  }
    }
}
