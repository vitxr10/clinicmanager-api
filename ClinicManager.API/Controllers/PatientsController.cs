using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll ()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpGet("document/{document}")]
        public IActionResult GetByDocument(string document)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(TesteClass test)
        {
            return CreatedAtAction(nameof(GetById), new { test.Id }, test);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TesteClass test)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
