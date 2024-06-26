﻿using AutoMapper;
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
                throw new DirectoryNotFoundException("Paciente não encontrado.");

            var doctor = await _userRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
                throw new DirectoryNotFoundException("Médico não encontrado.");

            var doctorAvailable = await _serviceRepository.DoctorAvailable(doctor.UserId, request.StartDate);
            if (!doctorAvailable)
                throw new Exception("Horário indisponível.");

            var service = _mapper.Map<Service>(request);
            var id = await _serviceRepository.CreateAsync(service);

            string location;

            if (service.Modality == Core.Enums.ServiceModalityEnum.Telemedicine)
                location = "Google Meets";
            else
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

            var googleEvent = await _calendarService.CreateEvent(calendarEvent);

            if (service.Modality == Core.Enums.ServiceModalityEnum.Telemedicine)
            {
                service.MeetingLink = googleEvent.HangoutLink;

                await _serviceRepository.SaveAsync();
            }

            return id;
        }
    }
}