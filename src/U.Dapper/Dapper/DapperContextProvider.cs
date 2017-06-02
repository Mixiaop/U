using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using U.Dapper;
using U.Dapper.Mapper;
using U.Dapper.Sql;
using U.Dapper.Startup.Configuration;

namespace U.Dapper
{
    public class DapperContextProvider : IDapperContextProvider
    {

        private readonly IDapperConfiguration _configuration;

        public IDapperImplementor Dapper { get; private set; }

        public DapperContextProvider(IDapperConfiguration configuration)
        {
            _configuration = configuration;
            var sqlGenerator = new SqlGeneratorImpl(configuration);
            Dapper = new DapperImplementor(sqlGenerator);
        }

        public IDbConnection GetConnection(bool readOnly = false)
        {
            SqlConnection conn = null;
            if (_configuration.OpenedReadAndWrite && readOnly)
            {
                conn = new SqlConnection(_configuration.ReadSqlConnectionString);
            }
            else
                conn = new SqlConnection(_configuration.SqlConnectionString);

            return conn;
        }
    }
}
