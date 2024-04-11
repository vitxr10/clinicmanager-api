using ClinicManager.Application.Commands.CreateService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Validators
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            RuleFor(s => s.PatientId)
                .NotEmpty().WithMessage("O preenchimento do ID do paciente é obrigatório.")
                .NotNull().WithMessage("O preenchimento do ID do paciente é obrigatório.");

            RuleFor(s => s.DoctorId)
                .NotEmpty().WithMessage("O preenchimento do ID do médico é obrigatório.")
                .NotNull().WithMessage("O preenchimento do ID do médico é obrigatório.");

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("O preenchimento do nome do serviço é obrigatório.")
                .NotNull().WithMessage("O preenchimento do nome do serviço é obrigatório.");

            RuleFor(s => s.PatientName)
                .NotEmpty().WithMessage("O preenchimento do nome do paciente é obrigatório.")
                .NotNull().WithMessage("O preenchimento do nome do paciente é obrigatório.");

            RuleFor(s => s.DoctorName)
                .NotEmpty().WithMessage("O preenchimento do nome do médico é obrigatório.")
                .NotNull().WithMessage("O preenchimento do nome do médico é obrigatório.");

            RuleFor(s => s.StartDate)
                .NotEmpty().WithMessage("O preenchimento da data de início é obrigatório.")
                .NotNull().WithMessage("O preenchimento da data de início é obrigatório.");

            RuleFor(s => s.Modality)
                .IsInEnum().WithMessage("Modalidade inválida.");


        }
    }
}
