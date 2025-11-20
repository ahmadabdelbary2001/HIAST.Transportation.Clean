using HIAST.Transportation.Domain.Entities;
using HIAST.Transportation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class BusConfiguration : IEntityTypeConfiguration<Bus>
{
    public void Configure(EntityTypeBuilder<Bus> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.LicensePlate)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Capacity)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired()
            .HasDefaultValue(BusStatus.Available);

        // Indexes
        builder.HasIndex(b => b.LicensePlate)
            .IsUnique();

        builder.HasIndex(b => b.Status);

        // Relationships
        builder.HasMany(b => b.Lines)
            .WithOne(l => l.Bus)
            .HasForeignKey(l => l.BusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}