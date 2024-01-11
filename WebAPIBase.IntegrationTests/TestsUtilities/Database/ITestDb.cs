namespace WebAPIBase.IntegrationTests.TestsUtilities.Database;

public interface ITestDb : IAsyncDisposable
{
    string ConnectionString { get; }
}