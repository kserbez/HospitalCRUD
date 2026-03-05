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
            // Departments - 10 entries
            migrationBuilder.Sql(@"
                INSERT INTO Departments (ShortName, LongName) VALUES
                ('CHIR',   'Allgemeine Chirurgie / General Surgery'),
                ('TRAV',   'Traumatologie und Orthopädie / Traumatology & Orthopedics'),
                ('NEUR',   'Neurologie / Neurology'),
                ('KARD',   'Kardiologie / Cardiology'),
                ('PULM',   'Pulmonologie / Pulmonology'),
                ('GAST',   'Gastroenterologie / Gastroenterology'),
                ('ONKO',   'Onkologie / Oncology'),
                ('INFE',   'Infektiologie / Infectious Diseases'),
                ('INTI',   'Intensivmedizin / Intensive Care Unit'),
                ('PÄDI',   'Pädiatrie / Pediatrics');
            ");

            // Patients - 20 entries
            migrationBuilder.Sql(@"
                INSERT INTO Patients (AdmissionNumber, PatientName, CurrentDepartmentId) VALUES
                ('A12345/26', 'Mustermann, Max',          1),
                ('B67890/26', 'Schmidt, Anna',            2),
                ('C11223/26', 'Müller, Peter',            3),
                ('D44556/26', 'Fischer, Maria',           4),
                ('E77889/26', 'Weber, Lukas',             5),
                ('F99001/26', 'Meyer, Sophie',            6),
                ('G22334/26', 'Wagner, Thomas',           7),
                ('H55667/26', 'Becker, Laura',            8),
                ('I88990/26', 'Schulz, Jonas',            9),
                ('J11223/26', 'Hoffmann, Lena',           10),
                ('K33445/26', 'Schäfer, Felix',           1),
                ('L66778/26', 'Koch, Emilia',             2),
                ('M99001/26', 'Bauer, Noah',              3),
                ('N22334/26', 'Richter, Mia',             4),
                ('O55667/26', 'Klein, Elias',             5),
                ('P88990/26', 'Wolf, Hannah',             6),
                ('Q11223/26', 'Schröder, Ben',            7),
                ('R33445/26', 'Neumann, Emma',            8),
                ('S66778/26', 'Schwarz, Paul',            9),
                ('T99001/26', 'Zimmermann, Sophia',       10);
            ");

            // PatientDepartmentAssignments - ~45 entries, multiple transfers per patient
            migrationBuilder.Sql(@"
                -- Patient A12345/26 (Mustermann, Max) – several transfers
                INSERT INTO PatientDepartmentAssignments (PatientAdmissionNumber, DepartmentId, AssignmentDate) VALUES
                ('A12345/26', 3,  '2025-10-15'),   -- Neurology
                ('A12345/26', 1,  '2025-11-20'),   -- General Surgery
                ('A12345/26', 9,  '2026-01-05'),   -- ICU
                ('A12345/26', 1,  '2026-02-10'),   -- back to Surgery

                -- Patient B67890/26 (Schmidt, Anna)
                ('B67890/26', 2,  '2025-09-01'),
                ('B67890/26', 2,  '2025-12-15'),
                ('B67890/26', 2,  '2026-02-28'),

                -- Patient C11223/26 (Müller, Peter) – many moves
                ('C11223/26', 4,  '2025-08-10'),
                ('C11223/26', 7,  '2025-09-25'),
                ('C11223/26', 9,  '2025-11-12'),
                ('C11223/26', 3,  '2026-01-18'),
                ('C11223/26', 3,  '2026-03-02'),

                -- Patients D–J – 2–4 transfers each
                ('D44556/26', 5,  '2025-12-05'),
                ('D44556/26', 1,  '2026-01-20'),
                ('E77889/26', 6,  '2025-11-10'),
                ('E77889/26', 6,  '2026-02-05'),
                ('F99001/26', 8,  '2025-10-20'),
                ('F99001/26', 10, '2026-03-01'),
                ('G22334/26', 7,  '2026-01-08'),
                ('G22334/26', 9,  '2026-02-15'),
                ('H55667/26', 2,  '2025-12-25'),
                ('H55667/26', 2,  '2026-03-04'),
                ('I88990/26', 1,  '2026-02-01'),
                ('J11223/26', 4,  '2025-11-30'),
                ('J11223/26', 4,  '2026-02-20'),

                -- Patients K–T – newer / fewer transfers
                ('K33445/26', 1,  '2026-02-12'),
                ('L66778/26', 2,  '2026-01-10'),
                ('M99001/26', 3,  '2025-12-28'),
                ('M99001/26', 7,  '2026-02-22'),
                ('N22334/26', 4,  '2026-03-03'),
                ('O55667/26', 5,  '2026-02-14'),
                ('P88990/26', 6,  '2025-12-01'),
                ('Q11223/26', 7,  '2026-01-25'),
                ('R33445/26', 8,  '2026-02-08'),
                ('S66778/26', 9,  '2026-03-05'),
                ('T99001/26', 10, '2026-02-25');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
