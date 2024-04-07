using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClinicManagerDbContext _dbContext;
        public UserRepository(ClinicManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.UserId;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
