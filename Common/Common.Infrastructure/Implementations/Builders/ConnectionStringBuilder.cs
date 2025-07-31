namespace Common.Infrastructure.Implementations.Builders;

public class ConnectionStringBuilder(string baseConnectionString)
{
    private string _server = string.Empty;
    private string _database = string.Empty;
    private string _user = string.Empty;
    private string _password = string.Empty;
    private readonly string _baseConnectionString = baseConnectionString;
    public ConnectionStringBuilder SetServer(string server)
    {
        _server = server;
        return this;
    }

    public ConnectionStringBuilder SetDatabase(string database)
    {
        _database = database;
        return this;
    }

    public ConnectionStringBuilder SetUser(string user)
    {
        _user = user;
        return this;
    }

    public ConnectionStringBuilder SetPassword(string password)
    {
        _password = password;
        return this;
    }
    public string Build()
    {
        return _baseConnectionString
            .Replace("{Server}", _server)
            .Replace("{Database}", _database)
            .Replace("{User}", _user)
            .Replace("{Password}", _password);
    }
}
