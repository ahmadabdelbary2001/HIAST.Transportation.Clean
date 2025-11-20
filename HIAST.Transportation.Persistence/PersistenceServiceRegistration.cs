using HIAST.Transportation.Application.Contracts.Persistence;
using HIAST.Transportation.Persistence.DatabaseContext;
using HIAST.Transportation.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HIAST.Transportation.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<TransportationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("TransportationConnectionString"),
                b => b.MigrationsAssembly(typeof(TransportationDbContext).Assembly.FullName)));

        // Register repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<ISupervisorRepository, SupervisorRepository>();
        services.AddScoped<IBusRepository, BusRepository>();
        services.AddScoped<IStopRepository, StopRepository>();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<ILineSubscriptionRepository, LineSubscriptionRepository>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}