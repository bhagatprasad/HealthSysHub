using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IDoctorManager
    {
        Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId);
        Task<List<Doctor>> GetDoctorsAsync(Guid hospitalId);
    }
}
