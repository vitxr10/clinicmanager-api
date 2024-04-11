using ClinicManager.Application.Commands.UpdateDoctor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Validators
{
    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(d => d.Phone)
                .NotNull().WithMessage("O preenchimento do telefone é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do telefone é obrigatório.");

            RuleFor(d => d.Email)
                .NotNull().WithMessage("O preenchimento do email é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(d => d.Solutions)
                .NotNull().WithMessage("O preenchimento das soluções é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento das soluções é obrigatório.");
        }
    }
}
