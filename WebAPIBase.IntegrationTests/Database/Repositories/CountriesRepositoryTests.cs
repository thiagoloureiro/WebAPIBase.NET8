using Microsoft.Extensions.DependencyInjection;
using WebAPIBase.Domain.Entities;
using WebAPIBase.Domain.Interfaces.Repositories;
using WebAPIBase.Infrastructure.Database.Context;
using WebAPIBase.IntegrationTests.Fixtures;

namespace WebAPIBase.IntegrationTests.Database.Repositories;

public class CountriesRepositoryTests : IClassFixture<WebAppWithDbFixture>, IAsyncLifetime
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICountriesRepository _countriesRepository;

    public CountriesRepositoryTests(WebAppWithDbFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
        _countriesRepository = _serviceProvider.GetRequiredService<ICountriesRepository>();
    }

    [Fact]
    public async Task GettingById_NonExistentId_ReturnsNull()
    {
        // Arrange
        const int id = 5;

        // Act
        var result = await _countriesRepository.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GettingById_ExistingId_ReturnsCountry()
    {
        // Arrange
        var countryToInsert = new Country { Id = 0, CountryCode = "code", Name = "name" };
        var insertedCountry = await InsertAndGetCountryAsync(countryToInsert);

        // Act
        var result = await _countriesRepository.GetByIdAsync(insertedCountry.Id);

        // Assert
        result.Should().BeEquivalentTo(insertedCountry);
    }

    [Fact]
    public async Task GettingAllCountries_EmptyTable_ReturnsEmptyEnumerable()
    {
        // Act
        var result = await _countriesRepository.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GettingAllCountries_NotEmptyTable_ReturnsMultipleEntries()
    {
        // Arrange
        var countryToInsert1 = new Country { Id = 0, CountryCode = "code1", Name = "name" };
        var countryToInsert2 = new Country { Id = 0, CountryCode = "ocode2", Name = "mName2" };
        var insertedCountry1 = await InsertAndGetCountryAsync(countryToInsert1);
        var insertedCountry2 = await InsertAndGetCountryAsync(countryToInsert2);

        // Act
        var result = await _countriesRepository.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo([insertedCountry1, insertedCountry2]);
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