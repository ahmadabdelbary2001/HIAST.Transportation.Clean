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

        builder.Property(l => l.BusId)
            .IsRequired();

        builder.Property(l => l.DriverId)
            .IsRequired();

        // Indexes
        builder.HasIndex(l => l.Name).IsUnique();
        builder.HasIndex(l => l.SupervisorId);
        builder.HasIndex(l => l.BusId);
        builder.HasIndex(l => l.DriverId);

        // Relationships

        builder.HasOne(l => l.Bus)
            .WithMany()
            .HasForeignKey(l => l.BusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Driver)
            .WithMany()
            .HasForeignKey(l => l.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.Stops)
            .WithOne(s => s.Line)
            .HasForeignKey(s => s.LineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.LineSubscriptions)
            .WithOne(ls => ls.Line)
            .HasForeignKey(ls => ls.LineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}