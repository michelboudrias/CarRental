using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Persistence;
using CarRental.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.DependencyInjection;

public static class InfrastructureRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
#if DEBUG
        services.AddDbContext<CarRentalDbContext>(options =>
                options.UseInMemoryDatabase("CarRentalDb").EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
            ServiceLifetime.Transient);
#endif
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IPricingDefinitionRepository, PricingDefinitionRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        return services;
    }
}