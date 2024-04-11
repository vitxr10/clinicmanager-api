using ClinicManager.Application.Commands.UpdatePatient;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Validators
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(d => d.Phone)
                .NotNull().WithMessage("O preenchimento do telefone é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do telefone é obrigatório.");

            RuleFor(d => d.Email)
                .NotNull().WithMessage("O preenchimento do email é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(d => d.Height)
                .NotNull().WithMessage("O preenchimento da altura é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da altura é obrigatório.");

            RuleFor(d => d.Weight)
                .NotNull().WithMessage("O preenchimento do peso é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do peso é obrigatório.");
        }
    }
}
