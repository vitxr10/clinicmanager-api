using ClinicManager.Application.Commands.AuthUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Validators
{
    public class AuthUserCommandValidator : AbstractValidator<AuthUserCommand>
    {
        public AuthUserCommandValidator()
        {
            RuleFor(a => a.Login)
                .NotNull().WithMessage("O preenchimento do login é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do login é obrigatório.");

            RuleFor(a => a.Password)
                .NotNull().WithMessage("O preenchimento da senha é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da senha é obrigatório.");
        }
    }
}
