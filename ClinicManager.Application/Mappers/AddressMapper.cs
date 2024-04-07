using AutoMapper;
using ClinicManager.Application.DTOs;
using ClinicManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Mappers
{
    public class AddressMapper : Profile
    {
        public AddressMapper()
        {
            CreateMap<AddressDTO, Address>();
            CreateMap<Address, AddressDTO>();
        }
    }
}
