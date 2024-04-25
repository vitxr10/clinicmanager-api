using ClinicManager.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.UpdateDoctor
{
    public class UpdateDoctorCommand : IRequest
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Solutions { get; set; }
        public AddressDTO AddressDTO { get; set; }
    }
}
