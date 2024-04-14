using AutoMapper;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using ClinicManager.Core.Repositories;
using ClinicManager.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public CreateDoctorCommandHandler(IUserRepository userRepository, IMapper mapper, IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = _mapper.Map<User>(request);

            doctor.Role = RoleEnum.Doctor;
            doctor.Password = _authService.ComputeSha256Hash(doctor.Password);

            await _userRepository.CreateAsync(doctor);

            var address = _mapper.Map<Address>(request.AddressDTO);
            doctor.Address = address;

            await _userRepository.SaveAsync();

            return doctor.UserId;
        }
    }
}
