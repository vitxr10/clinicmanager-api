using ClinicManager.Application.Commands.CreateDoctor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicManager.Application.Validators
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(d => d.FirstName)
               .NotNull().WithMessage("O preenchimento do nome é obrigatório.")
               .NotEmpty().WithMessage("O preenchimento do nome é obrigatório.");

            RuleFor(d => d.LastName)
                .NotNull().WithMessage("O preenchimento do sobrenome é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do sobrenome é obrigatório.");

            RuleFor(d => d.Birthday)
                .NotNull().WithMessage("O preenchimento da data de nascimento é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da data de nascimento é obrigatório.")
                .LessThan(DateTime.Now).WithMessage("Data de nascimento inválida.");

            RuleFor(d => d.Phone)
                .NotNull().WithMessage("O preenchimento do telefone é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do telefone é obrigatório.");

            RuleFor(d => d.Email)
                .NotNull().WithMessage("O preenchimento do email é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(d => d.Password)
                .NotNull().WithMessage("O preenchimento da senha é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da senha é obrigatório.")
                .Must(ValidPassword).WithMessage("A senha deve conter 8 caracteres, letras, números e caracteres especiais.");

            RuleFor(d => d.CPF)
                .NotNull().WithMessage("O preenchimento do cpf é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do cpf é obrigatório.")
                .Must(ValidCPF).WithMessage("CPF inválido.");

            RuleFor(d => d.BloodType)
                .IsInEnum().WithMessage("Tipo sanguíneo inválido.");

            RuleFor(d => d.Solutions)
                .NotNull().WithMessage("O preenchimento das soluções é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento das soluções é obrigatório.");

            RuleFor(d => d.CRM)
                .NotNull().WithMessage("O preenchimento do CRM é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do CRM é obrigatório.")
                .Must(ValidCRM).WithMessage("CRM inválido.");

            RuleFor(d => d.Specialty)
                .IsInEnum().WithMessage("Especialidade inválida");

            RuleFor(d => d.AddressDTO.CEP)
                .NotNull().WithMessage("O preenchimento do CEP é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do CEP é obrigatório.");

            RuleFor(d => d.AddressDTO.Number)
                .NotNull().WithMessage("O preenchimento do número do endereço é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do número do endereço é obrigatório.");

            RuleFor(d => d.AddressDTO.State)
                .NotNull().WithMessage("O preenchimento do estado é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do estado é obrigatório.");

            RuleFor(d => d.AddressDTO.City)
                .NotNull().WithMessage("O preenchimento da cidade é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da cidade é obrigatório.");

            RuleFor(d => d.AddressDTO.Neighborhood)
                .NotNull().WithMessage("O preenchimento do bairro é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do bairro é obrigatório.");

        }

        public static bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }

        public static bool ValidCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            int remainder = sum % 11;
            int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
            if (int.Parse(cpf[9].ToString()) != verificationDigit1)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            remainder = sum % 11;
            int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
            if (int.Parse(cpf[10].ToString()) != verificationDigit2)
                return false;

            return true;
        }

        public static bool ValidCRM(string crm)
        {
            crm = new string(crm.Where(char.IsDigit).ToArray());

            if (crm.Length < 5 || crm.Length > 6)
                return false;

            if (crm.Distinct().Count() == 1)
                return false;

            return true;
        }
    }
    
}
