using API.Data;
using API.Data.Entities;
using API.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

public class PatientService
{
    private readonly HospitalDbContext _context;

    public PatientService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<List<PatientDto>> GetAllAsync(CancellationToken ct = default)
    {
        var patients = await _context.Patients
            .AsNoTracking()
            .Include(p => p.CurrentDepartment)
            .ToListAsync(ct);

        return patients.Adapt<List<PatientDto>>();
    }

    public async Task<PatientDto?> GetByAdmissionNumberAsync(string admissionNumber, CancellationToken ct = default)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .Include(p => p.CurrentDepartment)
            .FirstOrDefaultAsync(p => p.AdmissionNumber == admissionNumber, ct);

        return patient?.Adapt<PatientDto>();
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto, CancellationToken ct = default)
    {
        if (await _context.Patients.AnyAsync(p => p.AdmissionNumber == dto.AdmissionNumber, ct))
        {
            throw new InvalidOperationException($"Patient with AdmissionNumber {dto.AdmissionNumber} already exists.");
        }

        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.ShortName == dto.DepartmentShortName, ct)
            ?? throw new KeyNotFoundException($"Department {dto.DepartmentShortName} not found");

        var patient = Patient.Create(dto.AdmissionNumber, dto.PatientName);
        patient.CurrentDepartmentId = department.Id;

        var assignment = PatientDepartmentAssignment.Create(
            patient,
            department,
            DateOnly.FromDateTime(DateTime.UtcNow));

        _context.Patients.Add(patient);
        _context.PatientDepartmentAssignments.Add(assignment);

        await _context.SaveChangesAsync(ct);

        var created = await _context.Patients
            .AsNoTracking()
            .Include(p => p.CurrentDepartment)
            .FirstAsync(p => p.AdmissionNumber == patient.AdmissionNumber, ct);

        return created.Adapt<PatientDto>();
    }

    public async Task UpdateAsync(string admissionNumber, UpdatePatientDto dto, CancellationToken ct = default)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.AdmissionNumber == admissionNumber, ct)
            ?? throw new KeyNotFoundException($"Patient {admissionNumber} not found");

        patient.PatientName = dto.PatientName;

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string admissionNumber, CancellationToken ct = default)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.AdmissionNumber == admissionNumber, ct)
            ?? throw new KeyNotFoundException($"Patient {admissionNumber} not found");

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync(ct);
    }

    public async Task TransferAsync( string admissionNumber, TransferPatientDto dto, CancellationToken ct = default)
    {
        var patient = await _context.Patients
            .Include(p => p.Assignments)
            .FirstOrDefaultAsync(p => p.AdmissionNumber == admissionNumber, ct)
            ?? throw new KeyNotFoundException("Patient not found");

        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.ShortName == dto.DepartmentShortName, ct)
            ?? throw new KeyNotFoundException("Department not found");

        var assignmentDate = dto.AssignmentDate ?? DateOnly.FromDateTime(DateTime.UtcNow);

        var currentAssignment = patient.Assignments
            .FirstOrDefault(a => a.LeftDate == null);

        if (currentAssignment != null)
        {
            currentAssignment.Leave(assignmentDate);
        }

        var newAssignment = PatientDepartmentAssignment.Create(
            patient,
            department,
            assignmentDate);

        _context.PatientDepartmentAssignments.Add(newAssignment);

        patient.CurrentDepartmentId = department.Id;

        await _context.SaveChangesAsync(ct);
    }
}