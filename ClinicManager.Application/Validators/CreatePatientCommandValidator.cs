using ClinicManager.Application.Commands.CreatePatient;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicManager.Application.Validators
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull().WithMessage("O preenchimento do nome é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do nome é obrigatório.");

            RuleFor(p => p.LastName)
                .NotNull().WithMessage("O preenchimento do sobrenome é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do sobrenome é obrigatório.");

            RuleFor(p => p.Birthday)
                .NotNull().WithMessage("O preenchimento da data de nascimento é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da data de nascimento é obrigatório.")
                .LessThan(DateTime.Now).WithMessage("Data de nascimento inválida.");

            RuleFor(p => p.Phone)
                .NotNull().WithMessage("O preenchimento do telefone é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do telefone é obrigatório.");

            RuleFor(p => p.Email)
                .NotNull().WithMessage("O preenchimento do email é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("O preenchimento da senha é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da senha é obrigatório.")
                .Must(ValidPassword).WithMessage("A senha deve conter 8 caracteres, letras, números e caracteres especiais.");

            RuleFor(p => p.CPF)
                .NotNull().WithMessage("O preenchimento do cpf é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do cpf é obrigatório.")
                .Must(ValidCPF).WithMessage("CPF inválido.");

            RuleFor(p => p.BloodType)
                .IsInEnum().WithMessage("Tipo sanguíneo inválido.");

            RuleFor(p => p.Height)
                .NotNull().WithMessage("O preenchimento da altura é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da altura é obrigatório.");

            RuleFor(p => p.Weight)
                .NotNull().WithMessage("O preenchimento do peso é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do peso é obrigatório.");

            RuleFor(p => p.AddressDTO.CEP)
                .NotNull().WithMessage("O preenchimento do CEP é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do CEP é obrigatório.");

            RuleFor(p => p.AddressDTO.Number)
                .NotNull().WithMessage("O preenchimento do número do endereço é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do número do endereço é obrigatório.");

            RuleFor(p => p.AddressDTO.State)
                .NotNull().WithMessage("O preenchimento do estado é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento do estado é obrigatório.");

            RuleFor(p => p.AddressDTO.City)
                .NotNull().WithMessage("O preenchimento da cidade é obrigatório.")
                .NotEmpty().WithMessage("O preenchimento da cidade é obrigatório.");

            RuleFor(p => p.AddressDTO.Neighborhood)
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
    }

}

