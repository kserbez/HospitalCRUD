namespace API.DTOs;
public class UpdateDepartmentDto
{
    public string? ShortName { get; set; }
    public string LongName { get; set; } = null!;
}