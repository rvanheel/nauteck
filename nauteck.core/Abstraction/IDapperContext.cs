namespace nauteck.core.Abstraction;

public interface IDapperContext
{
    public string ConnectionString { get; }
    IDbConnection Connection { get; }
}