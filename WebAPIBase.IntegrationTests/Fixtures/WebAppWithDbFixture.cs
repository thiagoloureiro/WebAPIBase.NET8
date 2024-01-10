using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using WebAPIBase.API;
using WebAPIBase.Infrastructure.Database.Context;
using WebAPIBase.IntegrationTests.TestsUtilities.Database;

namespace WebAPIBase.IntegrationTests.Fixtures;

public class WebAppWithDbFixture : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private ITestDb _testDb = default!; // initialization in initializeAsync
    public IServiceProvider ServiceProvider { get; private set; } = default!;
    private AsyncServiceScope _scope;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(opts => opts.ClearProviders());

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<WebAPIBaseDbContext>>();
            services.AddDbContext<WebAPIBaseDbContext>((_, options) => options.UseSqlServer(_testDb.ConnectionString));
        });
    }
    public async Task InitializeAsync()
    {
        _scope = Services.CreateAsyncScope();
        ServiceProvider = _scope.ServiceProvider;

        var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
        _testDb = await TestDbProvider.ProvideAsync(configuration);

        using var scope = ServiceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WebAPIBaseDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _scope.DisposeAsync();
        await _testDb.DisposeAsync();
    }
}