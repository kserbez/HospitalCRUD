namespace API.DTOs;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class CreatePatientDto
{
    public string AdmissionNumber { get; set; } = null!;
    [Required]
    public string PatientName { get; set; } = null!;
    public string DepartmentShortName { get; set; } = null!;
}