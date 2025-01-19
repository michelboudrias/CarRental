using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CarRental.Application.UseCases.EndRental.Pricing;

namespace CarRental.Application.DependencyInjection;

public static class ApplicationRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddScoped<IPriceCalculator, PriceCalculator>();
        return services;
    }
}