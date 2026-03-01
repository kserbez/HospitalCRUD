using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<PatientDepartmentAssignment> PatientDepartmentAssignments => Set<PatientDepartmentAssignment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(HospitalDbContext).Assembly
            );
        }

    }
}
