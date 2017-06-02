using System.Data;
using U.Dapper;

namespace U.Dapper
{
    public interface IDapperContextProvider
    {
        IDapperImplementor Dapper { get; }

        IDbConnection GetConnection(bool readOnly = false);
    }
}
