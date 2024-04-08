﻿using ClinicManager.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<ServiceDetailsViewModel>
    {
        public GetServiceByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
