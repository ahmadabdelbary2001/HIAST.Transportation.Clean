using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.LineId)
            .IsRequired();

        builder.Property(t => t.BusId)
            .IsRequired();

        builder.Property(t => t.DriverId)
            .IsRequired();

        builder.Property(t => t.ScheduledTime)
            .IsRequired();

        builder.Property(t => t.ActualStartTime)
            .IsRequired(false);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasDefaultValue(TripStatus.Scheduled);

        // Indexes
        builder.HasIndex(t => t.LineId);

        builder.HasIndex(t => t.BusId);

        builder.HasIndex(t => t.DriverId);

        builder.HasIndex(t => t.ScheduledTime);

        builder.HasIndex(t => t.Status);

        // Relationships
        builder.HasOne(t => t.Line)
            .WithMany(l => l.Trips)
            .HasForeignKey(t => t.LineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Bus)
            .WithMany(b => b.Trips)
            .HasForeignKey(t => t.BusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Driver)
            .WithMany(d => d.Trips)
            .HasForeignKey(t => t.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}