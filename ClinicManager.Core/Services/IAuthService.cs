using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string login, string role);
        string ComputeSha256Hash(string password);
    }
}
