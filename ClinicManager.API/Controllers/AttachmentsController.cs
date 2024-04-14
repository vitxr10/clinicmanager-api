﻿using ClinicManager.Application.Commands.Attachments;
using ClinicManager.Application.Commands.CreatePatient;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ClinicManager.API.Controllers
{
    [Route("api/attachments")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public AttachmentsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost("prescription")]
        public async Task<IActionResult> Post(CreatePrescriptionAttachmentCommand command)
        {
            try
            {
                var attachmentSended = await _mediatR.Send(command);

                if (!attachmentSended)
                {
                    return BadRequest("Não foi possível enviar a receita médica.");
                }

                return Ok();
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
