using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetAllPatients
{
    public class GetAllPatientsQuery : IRequest<List<PatientViewModel>>
    {
        public RoleEnum Role { get; set; } = RoleEnum.Patient;
    }
}
