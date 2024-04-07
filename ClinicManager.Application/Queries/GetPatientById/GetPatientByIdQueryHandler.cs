using ClinicManager.Application.DTOs;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDetailsViewModel>
    {
        private readonly IUserRepository _userRepository;
        public GetPatientByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PatientDetailsViewModel> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _userRepository.GetByIdAsync(request.Id);

            if (patient == null)
                throw new Exception("Paciente não encontrado.");

            var address = patient.Address;
            var addressDTO = new AddressDTO
                (
                    address.Number,
                    address.City,
                    address.State,
                    address.CEP,
                    address.Neighborhood
                );

            var patientDetailsViewModel = new PatientDetailsViewModel
                (
                    patient.UserId,
                    patient.FirstName,
                    patient.LastName,
                    patient.CPF,
                    patient.Birthday,
                    patient.Phone,
                    patient.Email,
                    patient.BloodType,
                    patient.Height,
                    patient.Weight,
                    addressDTO,
                    patient.Active,
                    patient.CreatedAt,
                    patient.UpdatedAt
                );

            return patientDetailsViewModel;
        }
    }
}
