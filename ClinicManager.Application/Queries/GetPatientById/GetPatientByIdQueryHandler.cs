using AutoMapper;
using ClinicManager.Application.DTOs;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDetailsViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetPatientByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PatientDetailsViewModel> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _userRepository.GetByIdAsync(request.Id);

            if (patient == null)
                throw new Exception("Paciente não encontrado.");

            var patientDetailsViewModel = _mapper.Map<PatientDetailsViewModel>(patient);

            var addressDTO = _mapper.Map<AddressDTO>(patient.Address);
            patientDetailsViewModel.AddressDTO = addressDTO;

            return patientDetailsViewModel;
        }
    }
}
