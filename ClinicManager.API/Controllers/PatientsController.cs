using ClinicManager.Application.Commands.CreatePatient;
using ClinicManager.Application.Queries.GetPatientById;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using ClinicManager.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public PatientsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        public IActionResult GetAll ()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetPatientByIdQuery(id);

                var patient = await _mediatR.Send(query);

                return Ok(patient);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("document/{document}")]
        public IActionResult GetByDocument(string document)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePatientCommand command)
        {
            var id = await _mediatR.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
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
