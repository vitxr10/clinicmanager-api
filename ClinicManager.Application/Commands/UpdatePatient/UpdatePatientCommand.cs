using ClinicManager.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.UpdatePatient
{
    public class UpdatePatientCommand : IRequest
    {

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}
