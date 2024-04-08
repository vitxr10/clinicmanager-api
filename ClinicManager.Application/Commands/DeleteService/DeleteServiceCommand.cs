using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.DeleteService
{
    public class DeleteServiceCommand : IRequest
    {
        public DeleteServiceCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
