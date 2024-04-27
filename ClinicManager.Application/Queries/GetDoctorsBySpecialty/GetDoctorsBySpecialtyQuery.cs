using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetDoctorsBySpecialty
{
    public class GetDoctorsBySpecialtyQuery : IRequest<List<DoctorViewModel>>
    {
        public GetDoctorsBySpecialtyQuery(SpecialtyEnum specialty)
        {
            Specialty = specialty;
        }

        public SpecialtyEnum Specialty { get; set; }
    }
}
