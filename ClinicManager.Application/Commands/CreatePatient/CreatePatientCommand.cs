using ClinicManager.Application.DTOs;
using ClinicManager.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.CreatePatient
{
    public class CreatePatientCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public RoleEnum Role { get; set; }
        public BloodTypeEnum BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public AddressDTO AddressDTO { get; set; }
    }
}
