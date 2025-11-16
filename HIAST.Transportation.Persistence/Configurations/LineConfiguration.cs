using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class LineConfiguration : IEntityTypeConfiguration<Line>
{
    public void Configure(EntityTypeBuilder<Line> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.Description)
            .HasMaxLength(1000);

        builder.Property(l => l.SupervisorId)
            .IsRequired();

        // Indexes
        builder.HasIndex(l => l.Name);

        builder.HasIndex(l => l.SupervisorId);

        // Relationships
        builder.HasOne(l => l.Supervisor)
            .WithMany(s => s.ManagedLines)
            .HasForeignKey(l => l.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.LineStops)
            .WithOne(ls => ls.Line)
            .HasForeignKey(ls => ls.LineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Trips)
            .WithOne(t => t.Line)
            .HasForeignKey(t => t.LineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Subscriptions)
            .WithOne(ls => ls.Line)
            .HasForeignKey(ls => ls.LineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}