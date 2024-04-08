using ClinicManager.Application.DTOs;
using ClinicManager.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CreateDoctor
{
    public class CreateDoctorCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public RoleEnum Role { get; set; } = RoleEnum.Doctor;
        public string Solutions { get; set; }
        public string CRM { get; set; }
        public SpecialtyEnum Specialty { get; set; }
        public BloodTypeEnum BloodType { get; set; }
        public AddressDTO AddressDTO { get; set; }
    }
}
