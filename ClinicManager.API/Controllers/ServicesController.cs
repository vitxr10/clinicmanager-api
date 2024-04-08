using ClinicManager.Application.Commands.CreateDoctor;
using ClinicManager.Application.Commands.CreateService;
using ClinicManager.Application.Commands.DeleteDoctor;
using ClinicManager.Application.Commands.DeleteService;
using ClinicManager.Application.Commands.FinishService;
using ClinicManager.Application.Commands.StartService;
using ClinicManager.Application.Commands.UpdateDoctor;
using ClinicManager.Application.Queries.GetAllDoctors;
using ClinicManager.Application.Queries.GetAllDoctorServices;
using ClinicManager.Application.Queries.GetAllPatientServices;
using ClinicManager.Application.Queries.GetAllServices;
using ClinicManager.Application.Queries.GetDoctorByDocument;
using ClinicManager.Application.Queries.GetDoctorById;
using ClinicManager.Application.Queries.GetServiceById;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllServicesQuery();

            var services = await _mediatR.Send(query);

            return Ok(services);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> GetAllPatientServices(int id)
        {
            var query = new GetAllPatientServicesQuery(id);

            var services = await _mediatR.Send(query);

            return Ok(services);
        }

        [HttpGet("doctors/{id}")]
        public async Task<IActionResult> GetAllDoctorServices(int id)
        {
            var query = new GetAllDoctorServicesQuery(id);

            var services = await _mediatR.Send(query);

            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateServiceCommand command)
        {
            var id = await _mediatR.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("start/{id}")]
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
