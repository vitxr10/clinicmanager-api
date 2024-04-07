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
        public CreatePatientCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new User
                (
                    request.FirstName,
                    request.LastName,
                    request.CPF,
                    request.Birthday,
                    request.Phone,
                    request.Email,
                    request.Password,
                    RoleEnum.Patient,
                    request.BloodType,
                    request.Height,
                    request.Weight
                );

            await _userRepository.CreateAsync(patient);

            var addressDTO = request.Address;
            var address = new Address
                (
                    patient.UserId,
                    addressDTO.Number,
                    addressDTO.City,
                    addressDTO.State,
                    addressDTO.CEP,
                    addressDTO.Neighborhood
                );

            patient.Address = address;
            await _userRepository.SaveAsync();

            return patient.UserId;
        }
    }
}
