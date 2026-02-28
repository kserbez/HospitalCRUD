using System;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Hospital.API.DTOs;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(ILogger<PatientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PatientDTO> Get()
        {
            var result = new List<PatientDTO>() {
                new PatientDTO {
                    PatientName = "Mustermann, Max",
                    AdmissionNumber = "88783/1"
                }, new PatientDTO {
                    PatientName = "Walter, Ken",
                    AdmissionNumber = "46213/2"
                }
            };

            return result;
        }

        [HttpGet("{id}")]
        public PatientDTO Get(int id)
        {
            return new PatientDTO
            {
                PatientName = "Mustermann, Max",
                AdmissionNumber = "88783/1"
            };
        }

        [HttpPost]
        public void Post([FromBody] PatientDTO value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PatientDTO value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
