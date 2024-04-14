using ClinicManager.Application.DTOs;
using ClinicManager.Application.Services;
using ClinicManager.Core.Repositories;
using MediatR;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Attachments
{
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        public CreatePrescriptionCommandHandler(IUserRepository userRepository, IPdfService pdfService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _pdfService = pdfService;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
                throw new DirectoryNotFoundException("Médico não encontrado.");

            var patient = await _userRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                throw new DirectoryNotFoundException("Paciente não encontrado.");

            var patientName = patient.FirstName + " " + patient.LastName;
            var doctorName = doctor.FirstName + " " + doctor.LastName;

            var prescriptionHeader = "Receita médica";
            var prescriptionContent = $"Para: {patientName} \n\nPrescrição:\n {request.Content}";
            var prescriptionFooter = $"Dr. {doctorName} \n\n CRM: {doctor.CRM}";

            var attachmentBytes = _pdfService.CreatePdf(prescriptionHeader, prescriptionContent, prescriptionFooter);
            if (attachmentBytes == null)
                throw new Exception("Não foi possível gerar o pdf.");

            AttachmentDTO attachment = new AttachmentDTO(attachmentBytes, "receitamedica.pdf");
            var toEmail = patient.Email;
            var subject = "Receita médica - Clínica";
            var body = $"Olá {patient.FirstName}, segue sua receita médica:";

            return _emailService.SendEmailWithAttachment(attachment, toEmail, subject, body);
        }
    }
}
