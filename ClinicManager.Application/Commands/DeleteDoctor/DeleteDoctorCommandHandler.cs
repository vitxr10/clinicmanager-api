using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
    {
        private readonly IUserRepository _userRepository;
        public DeleteDoctorCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.Id);

            if (doctor == null)
                throw new Exception("Médico não encontrado.");

            doctor.Delete();

            await _userRepository.SaveAsync();
        }
    }
}
