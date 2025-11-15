using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace HIAST.Transportation.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Scans the assembly for profiles and registers them.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Scans the assembly for handlers and registers them with MediatR.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Scans the assembly for validators and registers them with FluentValidation.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}