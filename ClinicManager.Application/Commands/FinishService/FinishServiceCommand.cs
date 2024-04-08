using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.FinishService
{
    public class FinishServiceCommand : IRequest
    {
        public FinishServiceCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
