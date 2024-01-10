using WebAPIBase.Application.Handlers;
using WebAPIBase.Domain.Entities;
using WebAPIBase.Domain.Interfaces.Repositories;

namespace WebAPIBase.UnitTests.Handlers;

public class CountriesHandlerTests
{
    private readonly ICountriesRepository _countriesRepositoryMock = Substitute.For<ICountriesRepository>();
    private readonly CountriesHandler _countriesHandler;

    public CountriesHandlerTests()
    {
        _countriesHandler = new(_countriesRepositoryMock);
    }

    [Fact]
    public async Task GettingAllCountries_ReturnsResultFromRepo()
    {
        // Arrange
        List<Country> repoResult = [
            new Country { Id = 1, CountryCode = "2", Name = "3" }
        ];
        _countriesRepositoryMock.GetAllAsync().Returns(repoResult);

        // Act
        var result = await _countriesRepositoryMock.GetAllAsync();

        // Assert
        result.Should().BeSameAs(repoResult);
    }
}