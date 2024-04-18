using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.DTOs
{
    public class CalendarEventDTO
    {
        public CalendarEventDTO(string summary, string description, string location, string patientEmail, string doctorEmail, DateTime start, DateTime end)
        {
            Summary = summary;
            Description = description;
            Location = location;
            PatientEmail = patientEmail;
            DoctorEmail = doctorEmail;
            Start = start;
            End = end;
        }

        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PatientEmail { get; set; }
        public string DoctorEmail { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
