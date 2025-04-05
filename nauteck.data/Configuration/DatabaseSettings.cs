namespace nauteck.data.Configuration;

public sealed record DatabaseSettings
{
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? Database { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }

    public string ConnectionString => $"Server={Host};port={Port};user id={User};password={Password};database={Database};SslMode=Required;CharSet=utf8mb4;Allow User Variables=True";
}
