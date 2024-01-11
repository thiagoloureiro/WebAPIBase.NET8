using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace WebAPIBase.IntegrationTests.TestsUtilities.Database;

public class TestMsSqlDb : ITestDb
{
    private readonly string _connectionString;
    public string ConnectionString => _connectionString;

    private TestMsSqlDb(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static TestMsSqlDb Create(string connectionString)
    {
        if (connectionString.Contains("Database", StringComparison.OrdinalIgnoreCase)
            || connectionString.Contains("Initial Catalog", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Connection shouldn't contain Database and Initial Catalog params",
                nameof(connectionString)
                );
        }

        var dbName = Guid.NewGuid().ToString();

        connectionString += $";Database={dbName}";

        return new TestMsSqlDb(connectionString);
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        await using var connection = new SqlConnection(ConnectionString);
        var serverConnection = new ServerConnection(connection);
        var server = new Server(serverConnection);

        server.KillDatabase(connection.Database);
    }
}