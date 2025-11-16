using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class LineStopConfiguration : IEntityTypeConfiguration<LineStop>
{
    public void Configure(EntityTypeBuilder<LineStop> builder)
    {
        builder.HasKey(ls => ls.Id);

        builder.Property(ls => ls.LineId)
            .IsRequired();

        builder.Property(ls => ls.StopId)
            .IsRequired();

        builder.Property(ls => ls.SequenceOrder)
            .IsRequired();

        // Indexes
        builder.HasIndex(ls => new { ls.LineId, ls.StopId })
            .IsUnique();

        builder.HasIndex(ls => new { ls.LineId, ls.SequenceOrder })
            .IsUnique();

        // Relationships
        builder.HasOne(ls => ls.Line)
            .WithMany(l => l.LineStops)
            .HasForeignKey(ls => ls.LineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ls => ls.Stop)
            .WithMany(s => s.LineStops)
            .HasForeignKey(ls => ls.StopId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}