using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HIAST.Transportation.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}