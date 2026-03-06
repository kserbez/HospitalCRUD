namespace API.DTOs;
public class TransferPatientDto
{
    public string DepartmentShortName { get; set; } = null!;
    public DateOnly? AssignmentDate { get; set; }
}