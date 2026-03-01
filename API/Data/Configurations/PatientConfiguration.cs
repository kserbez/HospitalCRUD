namespace API.Data.Configurations;

using API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.AdmissionNumber);

        builder.Property(p => p.AdmissionNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.PatientName)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasOne(p => p.CurrentDepartment)
            .WithMany()
            .HasForeignKey(p => p.CurrentDepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Assignments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientAdmissionNumber)
            .OnDelete(DeleteBehavior.Cascade);
    }
}