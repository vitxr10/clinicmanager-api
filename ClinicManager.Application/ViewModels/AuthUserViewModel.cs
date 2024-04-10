using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.ViewModels
{
    public class AuthUserViewModel
    {
        public AuthUserViewModel(string login, string token)
        {
            Login = login;
            Token = token;
        }

        public string Login { get; set; }
        public string Token { get; set; }
    }
}
