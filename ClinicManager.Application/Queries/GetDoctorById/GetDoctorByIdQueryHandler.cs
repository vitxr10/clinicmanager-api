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

namespace ClinicManager.Application.Queries.GetDoctorById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDetailsViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetDoctorByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDetailsViewModel> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.Id);

            if (doctor == null)
                throw new Exception("Médico não encontrado.");

            var doctorDetailsViewModel = _mapper.Map<DoctorDetailsViewModel>(doctor);

            var addressDTO = _mapper.Map<AddressDTO>(doctor.Address);
            doctorDetailsViewModel.AddressDTO = addressDTO;

            return doctorDetailsViewModel;
        }
    }
}
