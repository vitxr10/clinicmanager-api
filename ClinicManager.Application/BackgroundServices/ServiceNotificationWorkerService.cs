using ClinicManager.Application.Services;
using ClinicManager.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.BackgroundServices
{
    public class ServiceNotificationWorkerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceNotificationWorkerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync (CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var _serviceRepositoryScope = scope.ServiceProvider.GetRequiredService<IServiceRepository>();
                var _userRepositoryScope = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var _emailServiceScope = scope.ServiceProvider.GetRequiredService<IEmailService>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    var tomorrowServices = await _serviceRepositoryScope.GetAllTomorrowServices();

                    if (tomorrowServices.Count() != 0)
                    {
                        foreach (var service in tomorrowServices)
                        {
                            var patient = await _userRepositoryScope.GetByIdAsync(service.PatientId);
                            var toEmail = patient.Email;

                            var subject = $"Lembrete de {service.Name}";

                            var body = $"Prezado(a) {patient.FirstName}, \n\n";
                            body += $"Este é um lembrete sobre sua {service.Name} agendada para {service.StartDate.ToShortDateString()} às {service.StartDate.ToShortTimeString()}.\n";

                            if (service.Modality == Core.Enums.ServiceModalityEnum.Telemedicine)
                                body += $"Local: Google Meets {service.MeetingLink}\n\n";
                            else
                                body += $"Local: Clínica Excelência e Sáude, São Paulo SP, nº 123\n\n";

                            body += "Por favor, lembre-se de chegar alguns minutos antes do horário agendado. Caso precise reagendar ou cancelar sua consulta, entre em contato conosco o mais breve possível.\n\n";
                            body += "Se você tiver alguma dúvida ou precisar de assistência adicional, não hesite em nos contatar.\n\n";
                            body += "Atenciosamente, \nClínica Excelência e Saúde";

                            _emailServiceScope.Send(toEmail, subject, body.ToString());
                        }
                    }

                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
            }
        }
    }
}
