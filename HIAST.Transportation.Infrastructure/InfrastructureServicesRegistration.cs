using HIAST.Transportation.Application.Contracts.Logging;
using HIAST.Transportation.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace HIAST.Transportation.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register logger adapter
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}