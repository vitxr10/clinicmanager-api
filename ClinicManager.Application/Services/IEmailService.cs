using ClinicManager.Application.DTOs;

namespace ClinicManager.Application.Services
{
    public interface IEmailService
    {
        bool Send(string toEmail, string subject, string body);
        bool SendEmailWithAttachment(AttachmentDTO attachment, string toEmail, string subject, string body);
    }
}