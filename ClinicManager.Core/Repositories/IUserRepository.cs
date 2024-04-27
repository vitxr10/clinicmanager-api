using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user);
        Task<User> GetByLoginAndPasswordAsync(string login, string password);
        Task<List<User>> GetAllAsync(RoleEnum role);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByDocumentAsync(string document);
        Task<List<User>> GetDoctorsBySpecialtyAsync(SpecialtyEnum specialty);
        Task SaveAsync();
    }
}
