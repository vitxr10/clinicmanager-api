using ClinicManager.Application.Commands.CreateService;
using ClinicManager.Application.Commands.DeleteService;
using ClinicManager.Application.Commands.FinishService;
using ClinicManager.Application.Commands.StartService;
using ClinicManager.Application.Queries.GetAllDoctorServices;
using ClinicManager.Application.Queries.GetAllPatientServices;
using ClinicManager.Application.Queries.GetAllServices;
using ClinicManager.Application.Queries.GetServiceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public ServicesController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllServicesQuery();

            var services = await _mediatR.Send(query);

            return Ok(services);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Receptionist, Doctor, Patient")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetServiceByIdQuery(id);

                var service = await _mediatR.Send(query);

                return Ok(service);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("patients/{id}")]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> GetAllPatientServices(int id)
        {
            var query = new GetAllPatientServicesQuery(id);

            var services = await _mediatR.Send(query);

            return Ok(services);
        }

        [HttpGet("doctors/{id}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> GetAllDoctorServices(int id)
        {
            var query = new GetAllDoctorServicesQuery(id);

            var services = await _mediatR.Send(query);

            return Ok(services);
        }

        [HttpPost]
        [Authorize(Roles = "Receptionist, Patient")]
        public async Task<IActionResult> Post(CreateServiceCommand command)
        {
            var id = await _mediatR.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("start/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Start(int id)
        {
            try
            {
                var query = new StartServiceCommand(id);

                await _mediatR.Send(query);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("finish/{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Finish(int id)
        {
            try
            {
                var query = new FinishServiceCommand(id);

                await _mediatR.Send(query);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist, Patient, Doctor")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = new DeleteServiceCommand(id);

                await _mediatR.Send(query);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
