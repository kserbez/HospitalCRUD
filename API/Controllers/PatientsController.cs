namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using API.DTOs;
using API.Services;

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

    [HttpGet("by-admission")]
    public async Task<ActionResult<PatientDto>> GetByAdmissionNumber([FromQuery, Required] string admissionNumber)
    {
        var patient = await _service.GetByAdmissionNumberAsync(admissionNumber);

        if (patient == null)
            return NotFound();

        return Ok(patient);
    }



    [HttpGet("history")]
    public async Task<ActionResult<List<PatientHistoryDTO>>> GetHistory([FromQuery, Required] string admissionNumber)
    {
        try
        {
            return await _service.GetHistory(admissionNumber);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return BadRequest("An unexpected error occured");
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByAdmissionNumber), new { admissionNumber = created.AdmissionNumber }, created);
    }

    [HttpPut("by-admission")]
    public async Task<IActionResult> Update([FromQuery, Required] string admissionNumber, [FromBody] UpdatePatientDto dto)
    {
        try
        {
            await _service.UpdateAsync(admissionNumber, dto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return BadRequest("An unexpected error occured");
        }

        return NoContent();
    }

    [HttpDelete("by-admission")]
    public async Task<IActionResult> Delete([FromQuery, Required] string admissionNumber)
    {
        try
        {
            await _service.DeleteAsync(admissionNumber);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return BadRequest("An unexpected error occured");
        }

        return NoContent();
    }

    [HttpPost("transfer-by-admission")]
    public async Task<IActionResult> Transfer([FromQuery, Required] string admissionNumber, [FromBody] TransferPatientDto dto)
    {
        try
        {
            await _service.TransferAsync(admissionNumber, dto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch
        {
            return BadRequest("An unexpected error occured");
        }

        return NoContent();
    }

}