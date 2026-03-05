namespace API.DTOs;
public class PatientDto
{
    public string AdmissionNumber { get; set; } = null!;
    public string PatientName { get; set; } = null!;
    public string? CurrentDepartmentShortName { get; set; }
    public int? CurrentDepartmentId { get; set; }
}
