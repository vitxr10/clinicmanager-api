using AutoMapper;
using ClinicManager.Application.Commands.CreateService;
using ClinicManager.Application.ViewModels;
using ClinicManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Mappers
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<CreateServiceCommand, Service>()
                .ForMember(s => s.EndDate, options => options
                .MapFrom(csc => csc.StartDate.AddMinutes(30)));

            CreateMap<Service, ServiceViewModel>();
            CreateMap<Service, ServiceDetailsViewModel>();
        }
    }
}
