﻿using ClinicManager.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetAllPatientServices
{
    public class GetAllPatientServicesQuery : IRequest<List<ServiceViewModel>>
    {
        public GetAllPatientServicesQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
