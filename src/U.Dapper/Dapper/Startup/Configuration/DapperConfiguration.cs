
namespace U.Dapper.Startup.Configuration
{
    public class DapperConfiguration : DapperConfigurationBase, IDapperConfiguration
    {

        public DapperConfiguration(DapperSettings settings)
            : base()
        {
            OpenedReadAndWrite = false;
            SqlConnectionString = settings.SqlConnectionString;
            ReadSqlConnectionString = settings.SqlConnectionString;
        }
    }
}
