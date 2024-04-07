using AutoMapper;
using ClinicManager.Application.Commands.CreatePatient;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Mappers
{
    public class PatientMapper : Profile
    {
        public PatientMapper()
        {
            CreateMap<CreatePatientCommand, User>();
            CreateMap<User, PatientDetailsViewModel>();
        }
    }
}
