using Microsoft.Extensions.Configuration;

namespace WebAPIBase.IntegrationTests.TestsUtilities.Database;

public class TestDbProvider
{
    public static async Task<ITestDb> ProvideAsync(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("test-db");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return await TestContainersDb.CreateAsync();
        }
        return TestMsSqlDb.Create(connectionString);
    }
}