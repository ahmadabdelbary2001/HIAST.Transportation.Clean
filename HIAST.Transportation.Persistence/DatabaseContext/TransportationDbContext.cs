using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.DatabaseContext;

public class TransportationDbContext : DbContext
{
    public TransportationDbContext(DbContextOptions<TransportationDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Supervisor> Supervisors => Set<Supervisor>();
    public DbSet<Bus> Buses => Set<Bus>();
    public DbSet<Stop> Stops => Set<Stop>();
    public DbSet<Line> Lines => Set<Line>();
    public DbSet<LineStop> LineStops => Set<LineStop>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<LineSubscription> LineSubscriptions => Set<LineSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransportationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}