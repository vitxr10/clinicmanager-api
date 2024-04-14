using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Attachments
{
    public class CreateMedicalCertificateCommand : IRequest<bool>
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int SickLeaveDuration { get; set; }
        public string SickLeaveMotive { get; set; }
    }
}
