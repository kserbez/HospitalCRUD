using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_Patients_Departments_Assignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 10, nullable: false),
                    LongName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    AdmissionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PatientName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CurrentDepartmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.AdmissionNumber);
                    table.ForeignKey(
                        name: "FK_Patients_Departments_CurrentDepartmentId",
                        column: x => x.CurrentDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PatientDepartmentAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientAdmissionNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignmentDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDepartmentAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDepartmentAssignments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientDepartmentAssignments_Patients_PatientAdmissionNumber",
                        column: x => x.PatientAdmissionNumber,
                        principalTable: "Patients",
                        principalColumn: "AdmissionNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ShortName",
                table: "Departments",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientDepartmentAssignments_DepartmentId_AssignmentDate",
                table: "PatientDepartmentAssignments",
                columns: new[] { "DepartmentId", "AssignmentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDepartmentAssignments_PatientAdmissionNumber",
                table: "PatientDepartmentAssignments",
                column: "PatientAdmissionNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CurrentDepartmentId",
                table: "Patients",
                column: "CurrentDepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientDepartmentAssignments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
