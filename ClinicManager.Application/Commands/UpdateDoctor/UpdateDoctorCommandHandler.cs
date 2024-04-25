using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
    {
        private readonly IUserRepository _userRepository;
        public UpdateDoctorCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.Id);

            if (doctor == null)
                throw new Exception("Médico não encontrado.");

            var address = request.AddressDTO;

            doctor.Update(request.Phone, request.Email, request.Solutions, address.Number, address.City, address.State, address.CEP, address.Neighborhood);

            await _userRepository.SaveAsync();
        }
    }
}
