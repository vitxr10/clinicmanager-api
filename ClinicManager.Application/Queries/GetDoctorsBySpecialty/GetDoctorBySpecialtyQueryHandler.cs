using AutoMapper;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetDoctorsBySpecialty
{
    public class GetDoctorBySpecialtyQueryHandler : IRequestHandler<GetDoctorsBySpecialtyQuery, List<DoctorViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetDoctorBySpecialtyQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorViewModel>> Handle(GetDoctorsBySpecialtyQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _userRepository.GetDoctorsBySpecialtyAsync(request.Specialty);

            var doctorsViewModel = _mapper.Map<List<DoctorViewModel>>(doctors);

            return doctorsViewModel;
        }
    }
}
