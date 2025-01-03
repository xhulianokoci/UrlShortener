using Application.Interfaces;
using Application.IRepository;
using Application.Services;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //Register repositories
        services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

        //Register DbContext
        services.AddDbContext<ApplicationDbContext>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Register services
        services.AddScoped<IShortUrlService, ShortUrlService>();

        return services;
    }
}
