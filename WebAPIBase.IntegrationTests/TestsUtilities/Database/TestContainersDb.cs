using Testcontainers.MsSql;

namespace WebAPIBase.IntegrationTests.TestsUtilities.Database;

public class TestContainersDb : ITestDb
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();
    public string ConnectionString => _dbContainer.GetConnectionString();
    private TestContainersDb()
    {
    }

    public static async Task<TestContainersDb> CreateAsync()
    {
        var container = new TestContainersDb();
        await container._dbContainer.StartAsync();

        return container;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return _dbContainer.DisposeAsync();
    }
}