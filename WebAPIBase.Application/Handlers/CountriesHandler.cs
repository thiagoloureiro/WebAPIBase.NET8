using WebAPIBase.Application.Interfaces;
using WebAPIBase.Domain.Entities;
using WebAPIBase.Domain.Interfaces.Repositories;

namespace WebAPIBase.Application.Handlers;

public class CountriesHandler : ICountriesHandler
{
    private readonly ICountriesRepository _countriesRepository;

    public CountriesHandler(ICountriesRepository countriesRepository)
    {
        _countriesRepository = countriesRepository;
    }

    public async Task<Country> CreateAsync(string name, string countryCode)
    {
        var countryToInsert = new Country { Name = name, CountryCode = countryCode };
        var insertedCountry = await _countriesRepository.InsertAsync(countryToInsert);

        return insertedCountry;
    }

    public async Task<List<Country>> GetAllAsync()
        => await _countriesRepository.GetAllAsync();

    public async Task<Country?> GetByIdAsync(int id)
        => await _countriesRepository.GetByIdAsync(id);

    public async Task<Country> UpdateAsync(int id, string name, string countryCode)
    {
        var countryToUpdate = new Country { Id = id, CountryCode = countryCode, Name = name };
        var updatedCountry = await _countriesRepository.UpdateAsync(countryToUpdate);

        return updatedCountry;
    }

    public async Task DeleteAsync(int id)
        => await _countriesRepository.DeleteAsync(id);
}