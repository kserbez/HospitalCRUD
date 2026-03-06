namespace API.Services;

using API.Data;
using API.Data.Entities;
using API.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

public class DepartmentService
{
    private readonly HospitalDbContext _context;

    public DepartmentService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentDto>> GetAllAsync(CancellationToken ct = default)
    {
        var departments = await _context.Departments
            .AsNoTracking()
            .ToListAsync(ct);

        return departments.Adapt<List<DepartmentDto>>();
    }

    public async Task<DepartmentDto?> GetByShortNameAsync(string shortName, CancellationToken ct = default)
    {
        var department = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.ShortName == shortName, ct);

        return department?.Adapt<DepartmentDto>();
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, CancellationToken ct = default)
    {
        if (await _context.Departments.AnyAsync(d => d.ShortName == dto.ShortName, ct))
        {
            throw new InvalidOperationException($"Department with ShortName '{dto.ShortName}' already exists.");
        }

        var department = Department.Create(dto.ShortName, dto.LongName);

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(ct);

        return department.Adapt<DepartmentDto>();
    }

    public async Task UpdateAsync(string shortName, UpdateDepartmentDto dto, CancellationToken ct = default)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.ShortName == shortName, ct)
            ?? throw new KeyNotFoundException($"Department '{shortName}' not found");

        if (!string.IsNullOrEmpty(dto.ShortName) && dto.ShortName != shortName)
        {
            if (await _context.Departments.AnyAsync(d => d.ShortName == dto.ShortName, ct))
                throw new InvalidOperationException($"ShortName '{dto.ShortName}' already in use");

            department.ChangeShortName(dto.ShortName);
        }

        department.ChangeLongName(dto.LongName);

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string shortName, CancellationToken ct = default)
    {
        var department = await _context.Departments
            .Include(d => d.PatientDepartmentAssignments)
            .FirstOrDefaultAsync(d => d.ShortName == shortName, ct)
            ?? throw new KeyNotFoundException($"Department '{shortName}' not found");

        if (department.PatientDepartmentAssignments.Any())
        {
            throw new InvalidOperationException($"Cannot delete department '{shortName}' — it has assignment history.");
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<PatientDto>> GetPatientsOnDateAsync(string shortName, DateOnly date, CancellationToken ct = default)
    {
        var patients = await _context.PatientDepartmentAssignments
            .Where(a => a.Department.ShortName == shortName && a.AssignmentDate == date)
            .Select(a => a.Patient)
            .Distinct()
            .Include(p => p.CurrentDepartment)
            .AsNoTracking()
            .ToListAsync(ct);

        return patients.Adapt<List<PatientDto>>();
    }
}