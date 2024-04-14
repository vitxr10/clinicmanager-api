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
    public class CreateMedicalCertificateCommandHandler : IRequestHandler<CreateMedicalCertificateCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        public CreateMedicalCertificateCommandHandler(IUserRepository userRepository, IPdfService pdfService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _pdfService = pdfService;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreateMedicalCertificateCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
                throw new DirectoryNotFoundException("Médico não encontrado.");

            var patient = await _userRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                throw new DirectoryNotFoundException("Paciente não encontrado.");

            var patientName = patient.FirstName + " " + patient.LastName;
            var doctorName = doctor.FirstName + " " + doctor.LastName;
            var today = DateTime.Now.ToShortDateString();
            var finalSickLeaveDate = DateTime.Now.AddDays(request.SickLeaveDuration).ToShortDateString();

            var prescriptionHeader = "Atestado médico";
            var prescriptionContent = $"Atesto para os devidos fins que o Sr. (a) {patientName}, portador do CPF {patient.CPF}, esteve" +
                                $" sob cuidados médicos no dia {today} e deverá se afastar de suas atividades pelo período de " +
                                $"{today} até {finalSickLeaveDate} por motivo de {request.SickLeaveMotive}.";
            var prescriptionFooter = $"Dr. {doctorName} \n\n CRM: {doctor.CRM}";

            var attachmentBytes = _pdfService.CreatePdf(prescriptionHeader, prescriptionContent, prescriptionFooter);
            if (attachmentBytes == null)
                throw new Exception("Não foi possível gerar o pdf.");

            AttachmentDTO attachment = new AttachmentDTO(attachmentBytes, "atestadomedico.pdf");
            var toEmail = patient.Email;
            var subject = "Atestado médico - Clínica";
            var body = $"Olá {patient.FirstName}, segue seu atestado médico:";

            return _emailService.SendEmailWithAttachment(attachment, toEmail, subject, body);
        }
    }
}
