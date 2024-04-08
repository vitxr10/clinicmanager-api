using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.FinishService
{
    public class FinishServiceCommandHandler : IRequestHandler<FinishServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        public FinishServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(FinishServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.Id);

            if (service == null)
                throw new Exception("Consulta/Exame não encontrado.");

            service.Finish();

            await _serviceRepository.SaveAsync();
        }
    }
}
