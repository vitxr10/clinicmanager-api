using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Core.Entities
{
    public class User
    {
        public User()
        {
            Active = true;
            CreatedAt = DateTime.Now;
        }

        public User(string firstName, string lastName, string cPF, DateTime birthday, string phone, string email, string password, RoleEnum role, BloodTypeEnum bloodType, double height, double weight)
        {
            FirstName = firstName;
            LastName = lastName;
            CPF = cPF;
            Birthday = birthday;
            Phone = phone;
            Email = email;
            Password = password;
            Role = role;
            BloodType = bloodType;
            Height = height;
            Weight = weight;
            Active = true;
            CreatedAt = DateTime.Now;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public BloodTypeEnum BloodType { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Solutions { get; set; }
        public string? CRM { get; set; }
        public SpecialtyEnum? Specialty { get; set; }
        public Address Address { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Delete()
        {
            Active = false;
        }

        public void Update(string phone, string email, double height, double weight)
        {
            Phone = phone;
            Email = email;
            Height = height;
            Weight = weight;
            UpdatedAt = DateTime.Now;
        }

        public void Update(string phone, string email, string solutions)
        {
            Phone = phone;
            Email = email;
            Solutions = solutions;
            UpdatedAt = DateTime.Now;
        }

    }
}
