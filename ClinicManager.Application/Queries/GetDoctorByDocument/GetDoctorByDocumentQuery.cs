using ClinicManager.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetDoctorByDocument
{
    public class GetDoctorByDocumentQuery : IRequest<DoctorDetailsViewModel>
    {
        public GetDoctorByDocumentQuery(string document)
        {
            Document = document;
        }

        public string Document {  get; set; }
    }
}
