using ClinicManager.Application.Commands.CreatePatient;
using ClinicManager.Application.Commands.DeletePatient;
using ClinicManager.Application.Commands.UpdatePatient;
using ClinicManager.Application.Queries.GetAllPatients;
using ClinicManager.Application.Queries.GetPatientByDocument;
using ClinicManager.Application.Queries.GetPatientById;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using ClinicManager.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> GetAll ()
        {
            var query = new GetAllPatientsQuery();

            var patients = await _mediatR.Send(query);

            return Ok(patients);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Receptionist, Patient, Doctor")]
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
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> GetByDocument(string document)
        {
            try
            {
                var query = new GetPatientByDocumentQuery(document);

                var patient = await _mediatR.Send(query);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreatePatientCommand command)
        {
            var id = await _mediatR.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> Put(int id, UpdatePatientCommand command)
        {
            try
            {
                command.Id = id;

                await _mediatR.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeletePatientCommand(id);

                await _mediatR.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
