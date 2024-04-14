using ClinicManager.Application.DTOs;
using ClinicManager.Application.Services;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Attachments
{
    public class CreateExamRequestCommandHandler : IRequestHandler<CreateExamRequestCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        public CreateExamRequestCommandHandler(IUserRepository userRepository, IPdfService pdfService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _pdfService = pdfService;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreateExamRequestCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
                throw new DirectoryNotFoundException("Médico não encontrado.");

            var patient = await _userRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                throw new DirectoryNotFoundException("Paciente não encontrado.");

            var patientName = patient.FirstName + " " + patient.LastName;
            var doctorName = doctor.FirstName + " " + doctor.LastName;

            var prescriptionHeader = "Solicitação de exame";
            var prescriptionContent = $"Para: {patientName} \n\nSolicito os seguintes exames laboratoriais:\n {request.Content}";
            var prescriptionFooter = $"Dr. {doctorName} \n\n CRM: {doctor.CRM}";

            var attachmentBytes = _pdfService.CreatePdf(prescriptionHeader, prescriptionContent, prescriptionFooter);
            if (attachmentBytes == null)
                throw new Exception("Não foi possível gerar o pdf.");

            AttachmentDTO attachment = new AttachmentDTO(attachmentBytes, "solicitacaoexame.pdf");
            var toEmail = patient.Email;
            var subject = "Solicitação de exame - Clínica";
            var body = $"Olá {patient.FirstName}, segue sua solicitação de exame:";

            return _emailService.SendEmailWithAttachment(attachment, toEmail, subject, body);
        }
    }
}
