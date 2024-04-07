using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.DeletePatient
{
    public class DeletePatientCommand : IRequest
    {
        public DeletePatientCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
