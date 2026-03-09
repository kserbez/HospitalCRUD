
namespace API.DTOs;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class PatientHistoryDTO
{
    public string AdmissionNumber { get; set; }

    public string DepartmentShortName { get; set; }

    public DateOnly AssignedAt { get; set; }
}
