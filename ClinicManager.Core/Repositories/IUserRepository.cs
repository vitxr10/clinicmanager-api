using ClinicManager.Core.Entities;
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
        Task<User> GetByIdAsync(int id);
        Task SaveAsync();
    }
}
