using HealthSysHub.Web.DBConfiguration.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IHospitalTypeManager
    {
        Task<List<HospitalType>> GetHospitalTypesAsync();
        Task<HospitalType> GetHospitalTypeAsync(Guid hospitalTypeId);

        Task<HospitalType> InsertOrUpdateHospitalTypeAsync(HospitalType hospitalType);
    }
}
