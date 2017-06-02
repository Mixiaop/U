
namespace U.EntityFramework.Startup.Configuration
{
    public class EFConfiguration : IEFConfiguration
    {
        public EFConfiguration(EFSettings settings) {
            OpenedReadAndWrite = false;
            SqlConnectionString = settings.SqlConnectionString;
            ReadSqlConnectionString = settings.SqlConnectionString;
        }

        public string SqlConnectionString
        {
            get;
            private set;
        }

        public bool OpenedReadAndWrite
        {
            get;
            private set;
        }

        

        public string ReadSqlConnectionString
        {
            get;
            private set;
        }
    }
}
