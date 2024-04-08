using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.DeleteDoctor
{
    public class DeleteDoctorCommand : IRequest
    {
        public DeleteDoctorCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
