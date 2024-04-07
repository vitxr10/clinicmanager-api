using AutoMapper;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetAllPatients
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllPatientsQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<PatientViewModel>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _userRepository.GetAllAsync(request.Role);

            var patientsViewModel = _mapper.Map<List<PatientViewModel>>(patients);

            return patientsViewModel;
        }
    }
}
