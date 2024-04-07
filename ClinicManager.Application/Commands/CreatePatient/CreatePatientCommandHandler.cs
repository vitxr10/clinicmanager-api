using AutoMapper;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CreatePatient
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreatePatientCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<User>(request);

            await _userRepository.CreateAsync(patient);

            var address = _mapper.Map<Address>(request.AddressDTO);
            patient.Address = address;

            await _userRepository.SaveAsync();

            return patient.UserId;
        }
    }
}
