namespace nauteck.core.Implementation;

public sealed class DapperContext(string connectionString) : IDapperContext
{
    public string ConnectionString => connectionString;
    public IDbConnection Connection => new MySqlConnection(connectionString);
}