using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModels
{
    public class AuthUserViewModel
    {
        public AuthUserViewModel(string login, string role, string token)
        {
            Login = login;
            Role = role;
            Token = token;
        }

        public string Login { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
