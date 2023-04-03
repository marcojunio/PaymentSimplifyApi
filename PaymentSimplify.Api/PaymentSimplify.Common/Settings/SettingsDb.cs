namespace PaymentSimplify.Common.Settings;

public class SettingsDb
{
    private readonly string _connectionStringEnvironment;
    private string _connectionString = null!;

    public SettingsDb() => _connectionStringEnvironment = Environment.GetEnvironmentVariable("CONNECTION_STRING_MYSQL")!;

    public string ConnectionString
    {
        get => !string.IsNullOrEmpty(_connectionStringEnvironment)
            ? _connectionStringEnvironment
            : _connectionString;
        set => _connectionString = value;
    }
}