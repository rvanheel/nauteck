using System.Data;

using MySql.Data.MySqlClient;

using nauteck.core.Abstraction;

namespace nauteck.core.Implementation;

public sealed class DapperContext(string connectionString) : IDapperContext
{
    public string ConnectionString => connectionString;
    public IDbConnection Connection => new MySqlConnection(connectionString);
}