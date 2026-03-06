namespace API.Controllers;

using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/departments")]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetAll()
    {
        var departments = await _service.GetAllAsync();
        return Ok(departments);
    }

    [HttpGet("{shortName}")]
    public async Task<ActionResult<DepartmentDto>> GetByShortName(string shortName)
    {
        var department = await _service.GetByShortNameAsync(shortName);
        if (department is null)
            return NotFound();

        return Ok(department);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateDepartmentDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByShortName), new { shortName = created.ShortName }, created);
    }

    [HttpPut("{shortName}")]
    public async Task<IActionResult> Update(string shortName, [FromBody] UpdateDepartmentDto dto)
    {
        await _service.UpdateAsync(shortName, dto);
        return NoContent();
    }

    [HttpDelete("{shortName}")]
    public async Task<IActionResult> Delete(string shortName)
    {
        await _service.DeleteAsync(shortName);
        return NoContent();
    }

    [HttpGet("{shortName}/patients")]
    public async Task<ActionResult<List<PatientDto>>> GetPatientsOnDate(string shortName, [FromQuery] DateOnly date)
    {
        var patients = await _service.GetPatientsOnDateAsync(shortName, date);
        return Ok(patients);
    }
}