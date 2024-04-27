using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using ClinicManager.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.AuthUser
{
    public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, AuthUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public AuthUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<AuthUserViewModel> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            var login = request.Login;
            var password = _authService.ComputeSha256Hash(request.Password);

            var user = await _userRepository.GetByLoginAndPasswordAsync(login, password);
            var userRole = user.Role.ToString();

            if (user == null)
                throw new Exception("Login e/ou senha incorretos.");

            var token = _authService.GenerateJwtToken(user.CPF, user.Role.ToString());

            return new AuthUserViewModel(login, userRole, token);
        }
    }
}
