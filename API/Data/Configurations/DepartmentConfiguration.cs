using API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.ShortName)
            .HasMaxLength(10)
            .IsRequired()
            .IsUnicode(false);

        builder.HasIndex(d => d.ShortName)
            .IsUnique();

        builder.Property(d => d.LongName)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasMany(d => d.PatientDepartmentAssignments)
               .WithOne(a => a.Department)
               .HasForeignKey(a => a.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}