using WebAPIBase.Domain.Entities;

namespace WebAPIBase.Application.Interfaces;

public interface ICountriesHandler
{
    Task<Country> CreateAsync(string name, string countryCode);
    Task DeleteAsync(int id);
    Task<List<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task<Country> UpdateAsync(int id, string name, string countryCode);
}