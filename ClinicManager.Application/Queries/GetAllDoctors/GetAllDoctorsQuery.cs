using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetAllDoctors
{
    public class GetAllDoctorsQuery : IRequest<List<DoctorViewModel>>
    {
        public RoleEnum Role { get; set; } = RoleEnum.Doctor;
    }
}
