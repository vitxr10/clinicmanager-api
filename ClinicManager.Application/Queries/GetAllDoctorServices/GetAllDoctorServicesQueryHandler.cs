using AutoMapper;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetAllDoctorServices
{
    public class GetAllDoctorServicesQueryHandler : IRequestHandler<GetAllDoctorServicesQuery, List<ServiceViewModel>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public GetAllDoctorServicesQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;

        }

        public async Task<List<ServiceViewModel>> Handle(GetAllDoctorServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllDoctorServices(request.Id);

            var servicesViewModel = _mapper.Map<List<ServiceViewModel>>(services);

            return servicesViewModel;
        }
    }
}
