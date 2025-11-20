using HIAST.Transportation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIAST.Transportation.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.EmployeeId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Department)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ContactInfo)
            .IsRequired()
            .HasMaxLength(200);

        // Indexes
        builder.HasIndex(e => e.EmployeeId)
            .IsUnique();

        // Relationships
        builder.HasMany(e => e.LineSubscriptions)
            .WithOne(ls => ls.Employee)
            .HasForeignKey(ls => ls.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}