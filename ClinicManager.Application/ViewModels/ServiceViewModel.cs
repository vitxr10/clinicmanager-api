using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Name { get; set; }
        public string? MeetingLink { get; set; }
        public DateTime StartDate { get; set; }
        public ServiceStatusEnum Status { get; set; }
        public ServiceModalityEnum Modality { get; set; }
    }
}
