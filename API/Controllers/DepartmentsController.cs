using Hospital.API.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<DepartmentDTO> Get()
        {
            return new DepartmentDTO[] {
                new DepartmentDTO { ShortName = "CHIR", LongName = "Allgemeine Chirurgie"},
                new DepartmentDTO { ShortName = "DEP2", LongName = "Department 2"}
            };
        }

        [HttpGet("{id}")]
        public DepartmentDTO Get(int id)
        {
            return new DepartmentDTO { ShortName = "CHIR", LongName = "Allgemeine Chirurgie" };
        }

        [HttpPost]
        public void Post([FromBody] DepartmentDTO value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
