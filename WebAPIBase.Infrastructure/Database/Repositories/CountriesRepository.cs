using Microsoft.EntityFrameworkCore;
using WebAPIBase.Domain.Entities;
using WebAPIBase.Domain.Interfaces.Repositories;
using WebAPIBase.Infrastructure.Database.Context;

namespace WebAPIBase.Infrastructure.Database.Repositories;

public class CountriesRepository : ICountriesRepository
{
    private readonly WebAPIBaseDbContext _dbContext;

    public CountriesRepository(WebAPIBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Country> InsertAsync(Country country)
    {
        await _dbContext.AddAsync(country);
        await _dbContext.SaveChangesAsync();

        return country;
    }

    public async Task<List<Country>> GetAllAsync()
    {
        var result = await _dbContext.Countries.ToListAsync();

        return result;
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        var result = await _dbContext.Countries.SingleOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<Country> UpdateAsync(Country country)
    {
        _dbContext.Update(country);

        await _dbContext.SaveChangesAsync();

        return country;
    }

    public async Task DeleteAsync(int id)
    {
        _dbContext.Remove(new Country { Id = id, CountryCode = "", Name = "" });
        await _dbContext.SaveChangesAsync();
    }
}