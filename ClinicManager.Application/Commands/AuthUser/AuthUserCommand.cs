using ClinicManager.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.AuthUser
{
    public class AuthUserCommand : IRequest<AuthUserViewModel>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
