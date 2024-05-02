using ClinicManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Core.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync(string stringQuery);
        Task<Service> GetByIdAsync(int id);
        Task<List<Service>> GetAllPatientServices(int id);
        Task<List<Service>> GetAllDoctorServices(int id);
        Task<List<Service>> GetAllTomorrowServices();
        Task<bool> DoctorAvailable(int id, DateTime startDate);
        Task<int> CreateAsync(Service service);
        Task SaveAsync();
    }
}
