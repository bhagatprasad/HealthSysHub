using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IPatientTypeManager
    {
        Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType);
        Task<PatientType> GetPatientTypeByIdAsync(Guid PatientTypeId);
        Task<List<PatientType>> GetPatientTypesAsync();
    }
}
