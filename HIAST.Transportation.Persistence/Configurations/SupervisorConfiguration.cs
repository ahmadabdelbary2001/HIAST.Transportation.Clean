using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
{
    public void Configure(EntityTypeBuilder<Supervisor> builder)
    {
        builder.Property(s => s.EmployeeId).IsRequired(false);
        builder.HasIndex(s => s.EmployeeId);
        
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
            .WithMany() // An Employee has no direct navigation back to Supervisor
            .HasForeignKey(s => s.EmployeeId) // The FK is the 'EmployeeId' property
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(s => s.Lines)
            .WithOne(l => l.Supervisor)
            .HasForeignKey(l => l.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}