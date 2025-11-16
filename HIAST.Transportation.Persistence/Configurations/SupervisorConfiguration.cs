using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
{
    public void Configure(EntityTypeBuilder<Supervisor> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.ContactInfo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.EmployeeId)
            .IsRequired(false);

        // Indexes
        builder.HasIndex(s => s.EmployeeId);

        // Relationships
        builder.HasOne(s => s.Employee)
            .WithMany()
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(s => s.ManagedLines)
            .WithOne(l => l.Supervisor)
            .HasForeignKey(l => l.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}