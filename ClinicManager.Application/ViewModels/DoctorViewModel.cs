using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModels
{
    public class DoctorViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string? Solutions { get; set; }
        public string? CRM { get; set; }
        public SpecialtyEnum? Specialty { get; set; }
        public bool Active { get; set; }
    }
}
