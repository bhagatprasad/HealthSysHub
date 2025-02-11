using HealthSysHub.Web.UI.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IPatientTypeService
    {
        Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType);
        Task<PatientType> GetPatientTypeByIdAsync(Guid PatientTypeId);
        Task<List<PatientType>> GetPatientTypesAsync();
    }
}
