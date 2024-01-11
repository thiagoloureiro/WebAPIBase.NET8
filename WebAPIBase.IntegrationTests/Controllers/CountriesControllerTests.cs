using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using WebAPIBase.Domain.Entities;
using WebAPIBase.Infrastructure.Database.Context;
using WebAPIBase.IntegrationTests.Fixtures;

namespace WebAPIBase.IntegrationTests.Controllers;

public class CountriesControllerTests : IClassFixture<WebAppWithDbFixture>, IAsyncLifetime
{
    private readonly IServiceProvider _serviceProvider;
    private readonly WebAppWithDbFixture _app;

    public CountriesControllerTests(WebAppWithDbFixture fixture)
    {
        _app = fixture;
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task GettingById_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        using var client = _app.CreateClient();
        const int id = 5;

        // Act
        using var result = await client.GetAsync($"/api/Countries/{id}");

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GettingById_ExistingId_ReturnsCountry()
    {
        // Arrange
        using var client = _app.CreateClient();
        var countryToInsert = new Country { Id = 0, CountryCode = "code", Name = "name" };
        var insertedCountry = await InsertAndGetCountryAsync(countryToInsert);

        // Act
        using var result = await client.GetAsync($"/api/countries/{insertedCountry.Id}");

        // Assert
        result.Should().BeSuccessful();
        var content = await result.Content.ReadFromJsonAsync<Country>();
        content.Should().BeEquivalentTo(insertedCountry);
    }

    private async Task<Country> InsertAndGetCountryAsync(Country country)
    {
        var dbContext = _serviceProvider.GetRequiredService<WebAPIBaseDbContext>();

        dbContext.Add(country);
        await dbContext.SaveChangesAsync();

        return country;
    }

    public async Task DisposeAsync()
    {
        var dbContext = _serviceProvider.GetRequiredService<WebAPIBaseDbContext>();

        dbContext.Countries.RemoveRange(dbContext.Countries);

        await dbContext.SaveChangesAsync();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}