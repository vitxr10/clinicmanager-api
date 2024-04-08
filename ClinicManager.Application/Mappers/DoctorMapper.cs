using AutoMapper;
using ClinicManager.Application.Commands.CreateDoctor;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Mappers
{
    public class DoctorMapper : Profile
    {
        public DoctorMapper()
        {
            CreateMap<CreateDoctorCommand, User>();
            CreateMap<User, DoctorViewModel>();
            CreateMap<User, DoctorDetailsViewModel>();
        }
    }
}
