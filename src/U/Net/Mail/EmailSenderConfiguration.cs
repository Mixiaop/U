using U.Dependency;

namespace U.Net.Mail
{
    /// <summary>
    /// Implementionation of <see cref="IEmailSenderConfiguration"/> that read settings <see cref="EmailSenderSettings"/>
    /// </summary>
    public class EmailSenderConfiguration : IEmailSenderConfiguration, ITransientDependency
    {
        private readonly EmailSenderSettings _settings;
        public EmailSenderConfiguration(EmailSenderSettings settings)
        {
            _settings = settings;
        }
        public string DefaultFromAddress
        {
            get
            {
                return _settings.DefaultFromAddress;
            }
        }

        public string DefaultFromDisplayName
        {
            get
            {
                return _settings.DefaultFromDisplayName;
            }
        }

        public string SmtpHost
        {
            get
            {
                return _settings.SmtpHost;
            }
        }

        public int SmtpPort
        {
            get
            {
                return _settings.SmtpPort;
            }
        }

        public string SmtpUsername
        {
            get
            {
                return _settings.SmtpUsername;
            }
        }

        public string SmtpPassword
        {
            get
            {
                return _settings.SmtpPassword;
            }
        }

        public string SmtpDomain
        {
            get
            {
                return _settings.SmtpDomain;
            }
        }

        public bool EnableSsl
        {
            get
            {
                return _settings.EnableSsl;
            }
        }

        public bool UseDefaultCredentials
        {
            get
            {
                return _settings.UseDefaultCredentials;
            }
        }
    }
}
