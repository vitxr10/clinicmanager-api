using ClinicManager.Application.Commands.CreateDoctor;
using ClinicManager.Application.Commands.CreatePatient;
using ClinicManager.Application.Commands.DeleteDoctor;
using ClinicManager.Application.Commands.DeletePatient;
using ClinicManager.Application.Commands.UpdateDoctor;
using ClinicManager.Application.Commands.UpdatePatient;
using ClinicManager.Application.Queries.GetAllDoctors;
using ClinicManager.Application.Queries.GetAllPatients;
using ClinicManager.Application.Queries.GetDoctorByDocument;
using ClinicManager.Application.Queries.GetDoctorById;
using ClinicManager.Application.Queries.GetPatientByDocument;
using ClinicManager.Application.Queries.GetPatientById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public DoctorsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllDoctorsQuery();

            var doctors = await _mediatR.Send(query);

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Receptionist, Patient, Doctor")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetDoctorByIdQuery(id);

                var doctor = await _mediatR.Send(query);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("document/{document}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetByDocument(string document)
        {
            try
            {
                var query = new GetDoctorByDocumentQuery(document);

                var doctor = await _mediatR.Send(query);

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Post(CreateDoctorCommand command)
        {
            var id = await _mediatR.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> Put(int id, UpdateDoctorCommand command)
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
                var command = new DeleteDoctorCommand(id);

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
