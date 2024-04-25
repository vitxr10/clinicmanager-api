using AutoMapper;
using ClinicManager.Application.DTOs;
using ClinicManager.Application.Services;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ICalendarEventsService _calendarService;
        private readonly IUserRepository _userRepository;
        public CreateServiceCommandHandler(IServiceRepository serviceRepository, IMapper mapper, ICalendarEventsService calendarService, IUserRepository userRepository)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _calendarService = calendarService;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var patient = await _userRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                throw new Exception("Paciente não encontrado.");

            var doctor = await _userRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
                throw new Exception("Médico não encontrado.");

            var service = _mapper.Map<Service>(request);
            var id = await _serviceRepository.CreateAsync(service);

            string location;

            if (service.Modality == Core.Enums.ServiceModalityEnum.Telemedicine)
            {
                location = "Google Meets";
            }

            location = "Clínica Excelência e Sáude, São Paulo, SP, nº 123";


            var calendarEvent = new CalendarEventDTO
                (
                    service.Name,
                    service.Name,
                    location,
                    patient.Email,
                    doctor.Email,
                    service.StartDate,
                    service.EndDate
                );

            await _calendarService.CreateEvent(calendarEvent);

            return id;
        }
    }
}