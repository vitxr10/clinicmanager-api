using ClinicManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Core.Entities
{
    public class Service
    {
        public Service()
        {
            Status = ServiceStatusEnum.Pending;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ServiceStatusEnum Status { get; set; }
        public ServiceModalityEnum Modality { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Start()
        {
            Status = ServiceStatusEnum.Started;
            UpdatedAt = DateTime.Now;
        }

        public void Finish()
        {
            Status = ServiceStatusEnum.Finished;
            UpdatedAt = DateTime.Now;
        }

        public void Delete()
        {
            Status = ServiceStatusEnum.Cancelled;
            UpdatedAt = DateTime.Now;
        }
    }
}
