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
    public class PatientDetailsViewModel
    {
        public PatientDetailsViewModel(int userId, string firstName, string lastName, string cPF, DateTime birthday, string phone, string email, BloodTypeEnum bloodType, double height, double weight, AddressDTO address, bool active, DateTime createdAt, DateTime? updatedAt)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CPF = cPF;
            Birthday = birthday;
            Phone = phone;
            Email = email;
            BloodType = bloodType;
            Height = height;
            Weight = weight;
            Address = address;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public BloodTypeEnum BloodType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AddressDTO Address { get; set; }

    }
}
