using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPIBase.Domain.Interfaces.Repositories;
using WebAPIBase.Infrastructure.Database.Context;
using WebAPIBase.Infrastructure.Database.Repositories;

namespace WebAPIBase.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddDbContext<WebAPIBaseDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Database") ?? throw new ApplicationException("Database:ConnectionString is null");

                options.UseSqlServer(connectionString);
            })
            .AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICountriesRepository, CountriesRepository>();
        return services;
    }
}