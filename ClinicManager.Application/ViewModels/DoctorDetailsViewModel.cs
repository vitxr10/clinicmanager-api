using ClinicManager.Application.DTOs;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModels
{
    public class DoctorDetailsViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public BloodTypeEnum BloodType { get; set; }
        public string? Solutions { get; set; }
        public string? CRM { get; set; }
        public SpecialtyEnum? Specialty { get; set; }
        public bool Active { get; set; }
        public AddressDTO AddressDTO { get; set; }

    }
}
