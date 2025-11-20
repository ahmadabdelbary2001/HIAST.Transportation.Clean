using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.LineId)
            .IsRequired();

        builder.Property(s => s.SequenceOrder)
            .IsRequired();

        // Indexes
        builder.HasIndex(s => s.LineId);
        builder.HasIndex(s => new { s.LineId, s.SequenceOrder })
            .IsUnique();

        // Relationships
        builder.HasOne(s => s.Line)
            .WithMany(l => l.Stops)
            .HasForeignKey(s => s.LineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}