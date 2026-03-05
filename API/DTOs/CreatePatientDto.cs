namespace API.DTOs;

public class CreatePatientDto
{
    public string AdmissionNumber { get; set; } = null!;
    public string PatientName { get; set; } = null!;
    public string DepartmentShortName { get; set; } = null!;
}