using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Persistence.DatabaseContext;

public class TransportationDbContext : DbContext
{
    private readonly IUserService _userService;

    public TransportationDbContext(DbContextOptions<TransportationDbContext> options, IUserService userService) : base(options)
    {
        _userService = userService;
    }

    // public DbSet<Employee> Employees => Set<Employee>(); // Removed
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Bus> Buses { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<Line> Lines { get; set; }
    public DbSet<LineSubscription> LineSubscriptions { get; set; }
    public DbSet<Notification> Notifications { get; set; }

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
                    entry.Entity.CreatedBy = _userService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = _userService.UserId;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}