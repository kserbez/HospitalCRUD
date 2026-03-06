using API.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly PatientService _service;

    public PatientsController(PatientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientDto>>> GetAll()
    {
        var patients = await _service.GetAllAsync();
        return Ok(patients);
    }

    [HttpGet("{admissionNumber}")]
    public async Task<ActionResult<PatientDto>> GetByNumber(string admissionNumber)
    {
        var patient = await _service.GetByAdmissionNumberAsync(admissionNumber);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByNumber), new { admissionNumber = created.AdmissionNumber }, created);
    }

    [HttpPut("{admissionNumber}")]
    public async Task<IActionResult> Update(string admissionNumber, [FromBody] UpdatePatientDto dto)
    {
        await _service.UpdateAsync(admissionNumber, dto);
        return NoContent();
    }

    [HttpDelete("{admissionNumber}")]
    public async Task<IActionResult> Delete(string admissionNumber)
    {
        await _service.DeleteAsync(admissionNumber);
        return NoContent();
    }

    [HttpPost("{admissionNumber}/transfer")]
    public async Task<IActionResult> Transfer(string admissionNumber, [FromBody] TransferPatientDto dto)
    {
        await _service.TransferAsync(admissionNumber, dto);
        return NoContent();
    }
}