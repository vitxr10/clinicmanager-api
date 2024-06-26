﻿using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using Google.Apis.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IUserRepository _userRepository;
        public UpdatePatientCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _userRepository.GetByIdAsync(request.Id);

            if (patient == null)
                throw new Exception("Paciente não encontrado.");

            patient.Update(request.Phone, request.Email, request.Height, request.Weight);

            var address = request.AddressDTO;
            patient.Address.Update(address.Number, address.City, address.State, address.CEP, address.Neighborhood);

            await _userRepository.SaveAsync();
        }
    }
}
