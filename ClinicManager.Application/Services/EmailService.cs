using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClinicManager.Application.DTOs;
using System.Runtime;

namespace ClinicManager.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Send(string toEmail, string subject, string body)
        {
            try
            {
                string host = _configuration["SMTP:Host"];
                string name = _configuration["SMTP:Name"];
                string username = _configuration["SMTP:UserName"];
                string password = _configuration["SMTP:Password"];
                int port = int.Parse(_configuration["SMTP:Port"]);

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, name)
                };

                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendEmailWithAttachment(AttachmentDTO attachment, string toEmail, string subject, string body)
        {
            try
            {
                string host = _configuration["SMTP:Host"];
                string name = _configuration["SMTP:Name"];
                string username = _configuration["SMTP:UserName"];
                string password = _configuration["SMTP:Password"];
                int port = int.Parse(_configuration["SMTP:Port"]);

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(username);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.Priority = MailPriority.High;

                    using (MemoryStream ms = new MemoryStream(attachment.Bytes))
                    {
                        mail.Attachments.Add(new Attachment(ms, attachment.Name, "application/pdf"));

                        using (SmtpClient smtp = new SmtpClient(host, port))
                        {
                            smtp.Credentials = new NetworkCredential(username, password);
                            smtp.EnableSsl = true;

                            smtp.Send(mail);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
