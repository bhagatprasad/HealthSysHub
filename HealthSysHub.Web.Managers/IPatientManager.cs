using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IPatientManager
    {
        Task<Patient> InsertOrUpdatePatientAsync(Patient patient);
        Task <List<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientByIdAsync(Guid patientId);
        Task <List<Patient>> GetPatientByIdHospitalAsync(Guid hospitalId);
    }
}
