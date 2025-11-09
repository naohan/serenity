using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using serenity.Application;
using serenity.Application.Configuration;
using serenity.Infrastructure.Configuration;

namespace serenity.Configuration;

/// <summary>
/// Centralised dependency registration for the Serenity API.
/// </summary>
public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddSerenityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddSerenityApplication();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>();
        });

        return services;
    }
}

