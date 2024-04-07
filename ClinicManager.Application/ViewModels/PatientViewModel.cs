using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModels
{
    public class PatientViewModel
    {
        public PatientViewModel(int userId, string firstName, string lastName, string cPF, string email, bool active)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CPF = cPF;
            Email = email;
            Active = active;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
