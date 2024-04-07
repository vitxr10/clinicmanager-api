using AutoMapper;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.DeletePatient
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IUserRepository _userRepository;
        public DeletePatientCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _userRepository.GetByIdAsync(request.Id);

            if (patient == null)
                throw new Exception("Paciente não encontrado.");

            patient.Delete();

            await _userRepository.SaveAsync();
        }
    }
}
