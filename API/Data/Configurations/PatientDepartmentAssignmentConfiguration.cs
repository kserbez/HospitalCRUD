using API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations;


public class PatientDepartmentAssignmentConfiguration : IEntityTypeConfiguration<PatientDepartmentAssignment>
{
    public void Configure(EntityTypeBuilder<PatientDepartmentAssignment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.PatientAdmissionNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.AssignmentDate)
            .IsRequired();

        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Assignments)
            .HasForeignKey(a => a.PatientAdmissionNumber)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Department)
            .WithMany(d => d.PatientDepartmentAssignments)
            .HasForeignKey(a => a.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.DepartmentId, a.AssignmentDate });
    }
}