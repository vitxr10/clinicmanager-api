using AutoMapper;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
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
        public CreateDoctorCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = _mapper.Map<User>(request);

            await _userRepository.CreateAsync(doctor);

            var address = _mapper.Map<Address>(request.AddressDTO);
            doctor.Address = address;

            await _userRepository.SaveAsync();

            return doctor.UserId;
        }
    }
}
