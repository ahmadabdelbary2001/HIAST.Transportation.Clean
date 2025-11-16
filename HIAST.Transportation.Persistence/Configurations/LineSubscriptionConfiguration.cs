using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class LineSubscriptionConfiguration : IEntityTypeConfiguration<LineSubscription>
{
    public void Configure(EntityTypeBuilder<LineSubscription> builder)
    {
        builder.HasKey(ls => ls.Id);

        builder.Property(ls => ls.EmployeeId)
            .IsRequired();

        builder.Property(ls => ls.LineId)
            .IsRequired();

        builder.Property(ls => ls.StartDate)
            .IsRequired();

        builder.Property(ls => ls.EndDate)
            .IsRequired(false);

        // Indexes
        builder.HasIndex(ls => ls.EmployeeId);

        builder.HasIndex(ls => ls.LineId);

        builder.HasIndex(ls => new { ls.EmployeeId, ls.LineId, ls.StartDate });

        // Relationships
        builder.HasOne(ls => ls.Employee)
            .WithMany(e => e.Subscriptions)
            .HasForeignKey(ls => ls.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ls => ls.Line)
            .WithMany(l => l.Subscriptions)
            .HasForeignKey(ls => ls.LineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}