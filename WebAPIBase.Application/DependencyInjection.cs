using Microsoft.Extensions.DependencyInjection;
using WebAPIBase.Application.Handlers;
using WebAPIBase.Application.Interfaces;

namespace WebAPIBase.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICountriesHandler, CountriesHandler>();
        return services;
    }
}