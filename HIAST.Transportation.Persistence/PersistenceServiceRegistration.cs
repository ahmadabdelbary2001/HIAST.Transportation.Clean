using Microsoft.Extensions.DependencyInjection;

namespace HIAST.Transportation.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        return services;
    }
}