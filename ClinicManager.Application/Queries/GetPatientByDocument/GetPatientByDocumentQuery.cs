using ClinicManager.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Queries.GetPatientByDocument
{
    public class GetPatientByDocumentQuery : IRequest<PatientDetailsViewModel>
    {
        public GetPatientByDocumentQuery(string document)
        {
            Document = document;
        }

        public string Document {  get; set; }
    }
}
