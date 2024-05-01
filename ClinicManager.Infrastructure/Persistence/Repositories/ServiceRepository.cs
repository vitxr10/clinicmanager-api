using ClinicManager.Core.Entities;
using ClinicManager.Core.Repositories;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ClinicManagerDbContext _dbContext;
        public ServiceRepository(ClinicManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Service service)
        {
            await _dbContext.Services.AddAsync(service);
            await _dbContext.SaveChangesAsync();

            return service.Id;
        }

        public async Task<bool> DoctorAvailable(int id, DateTime startDate)
        {
            var endDate = startDate.AddMinutes(30);

            var unavailable = await _dbContext.Services.AnyAsync(s => s.DoctorId == id && startDate >= s.StartDate && startDate <= s.EndDate
            || endDate >= s.StartDate && endDate <= s.EndDate);

            if (unavailable)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Service>> GetAllAsync(string stringQuery)
        {
            //if (!stringQuery.IsNullOrEmpty())
            //    return await _dbContext.Services.Contains(stringQuery);


            return await _dbContext.Services.ToListAsync();
        }

        public async Task<List<Service>> GetAllDoctorServices(int id)
        {
            return await _dbContext.Services.Where(s => s.DoctorId == id).ToListAsync();
        }

        public async Task<List<Service>> GetAllPatientServices(int id)
        {
            return await _dbContext.Services.Where(s => s.PatientId == id).ToListAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _dbContext.Services.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
