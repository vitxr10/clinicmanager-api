using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.StartService
{
    public class StartServiceCommandHandler : IRequestHandler<StartServiceCommand>
    {

        private readonly IServiceRepository _serviceRepository;
        public StartServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(StartServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.Id);

            if (service == null)
                throw new Exception("Consulta/Exame não encontrado.");

            service.Start();

            await _serviceRepository.SaveAsync();
        }
    }
}
