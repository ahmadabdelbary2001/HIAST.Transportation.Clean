using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Latitude)
            .IsRequired()
            .HasPrecision(10, 7);

        builder.Property(s => s.Longitude)
            .IsRequired()
            .HasPrecision(10, 7);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(s => s.Name);

        builder.HasIndex(s => new { s.Latitude, s.Longitude });

        // Relationships
        builder.HasMany(s => s.LineStops)
            .WithOne(ls => ls.Stop)
            .HasForeignKey(ls => ls.StopId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}