using WebAPIBase.Domain.Entities;

namespace WebAPIBase.Domain.Interfaces.Repositories;

public interface ICountriesRepository
{
    Task DeleteAsync(int id);
    Task<List<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task<Country> InsertAsync(Country country);
    Task<Country> UpdateAsync(Country country);
}