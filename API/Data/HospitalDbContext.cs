using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<PatientDepartmentAssignment> PatientDepartmentAssignments => Set<PatientDepartmentAssignment>();

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(HospitalDbContext).Assembly
            );
        }

    }
}
