using AutoMapper;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceDetailsViewModel>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;

        }

        public async Task<ServiceDetailsViewModel> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.Id);

            if (service == null)
                throw new Exception("Consulta/Exame não encontrado.");

            var serviceDetailsViewModel = _mapper.Map<ServiceDetailsViewModel>(service);

            return serviceDetailsViewModel;
        }
    }
}
