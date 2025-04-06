using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IPatientVitalManager
    {
        Task<PatientVital> InsertOrUpdatePatientVitalAsync(PatientVital patientVital);
        Task<List<PatientVital>> GetPatientVitalsAsync();
        Task<PatientVital> GetPatientVitalAsync(Guid vitalId);
        Task<List<PatientVital>> GetPatientVitalByHospitalAsync(Guid hospitalId);
        Task<List<PatientVital>> GetPatientVitalByPatientAsync(Guid patientId);
    } 
}
