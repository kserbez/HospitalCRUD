using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Departments (ShortName, LongName) VALUES
                ('CHIR', 'Allgemeine Chirurgie'),
                ('INT',  'Innere Medizin'),
                ('NEUR', 'Neurologie');

                INSERT INTO Patients (AdmissionNumber, PatientName, CurrentDepartmentId) VALUES
                ('88783/1', 'Mustermann, Max', 1),
                ('99214/2', 'Schmidt, Anna', 2);
        
                INSERT INTO PatientDepartmentAssignments (PatientAdmissionNumber, DepartmentId, AssignmentDate) VALUES
                ('88783/1', 1, '2026-02-15'),
                ('88783/1', 2, '2026-03-01'),
                ('99214/2', 2, '2026-01-10');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
